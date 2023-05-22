using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class NPC : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform[] waypoints;
    public GameObject npcPrefab; // Prefab for the npc
    public List<Npc> npcList = new List<Npc>();
    public float spawnTime = 5f;
    public ObjectSelector objectSelector;
    public Order orderClass;

    private Vector3 _spawnPosition;
    private int _count = 0;
    private int _countStatus = 0;
    private bool _spawnMax = false;

    public class Npc
    {
        public int ID { get; set; }
        public GameObject Object { get; set; }
        public string Name { get; set; }
        public List<string> Order { get; set; }
        //OrderStatus = true means that the order has not yet been processed
        public bool OrderStatus { get; set; }


    }


    private void Start()
    {
        _spawnPosition = new Vector3(-399.7f, 1.3f, -14.0f); //Spawn position npc
        StartCoroutine(SpawnNPC());
    }

    private void Update()
    {


        //Handle mouse click on npc
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Define clickedObject
                GameObject clickedObject = hit.collider.gameObject;
                //If you click on the npc, it moves to the next waypoint (waypoint end)
                if (clickedObject != null && clickedObject.name.Contains("Customer") && npcList != null
                    && objectSelector.listSelectedObjects != null)
                {

                    if (npcList.Count > 0)
                    {
                        for (int i = 0; i < npcList.Count; i++)
                        {
                            if (npcList[i].Object != null)
                            {
                                if (npcList[i].Name == clickedObject.name)
                                {
                                    if (CheckMatch(npcList[i].Order, objectSelector.listSelectedObjects) == true)
                                    {
                                        MoveNPCToEnd(clickedObject);
                                        objectSelector.Delete();
                                        npcList[i].OrderStatus = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
       
        //Go through all npc and see if they need to be deleted or collide with each other
        if (npcList != null)
        {
            for (int i = 0; i < npcList.Count; i++)
            {
                if (npcList[i].Object != null)
                {
                    CheckCollisionNPC(npcList[i].Object, npcList[i].OrderStatus, i, npcList[i].OrderStatus);
                    DestroyNPC(npcList[i].Object, npcList[i].Object.transform.position, waypoints[waypoints.Length - 1].position, npcList[i]);
                }
            }
        }

        NPCOrderStatus();
    }

    /*
     Checks the NPC's order list with the player's selection (also a list).
     */

    bool CheckMatch(List<string> l1, List<string> l2)
    {
       
        if (l1.Count == 0 || l2.Count == 0)
        {
            return false;
        }

        if (l1.Count != l2.Count)
        {
            return false;
        }


        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
            {
                return false;
            }

        }
        //The lists are equal
        return true;
    }

    private void DestroyNPC(GameObject npc, Vector3 positionNPC, Vector3 endTarget, Npc npcListNPC)
    {

        if (Vector3.Distance(positionNPC, endTarget) < 2.5f && npc != null)
        {
            //Debug.Log("Destroyed " + npc.name);
            npcList.Remove(npcListNPC);
            Destroy(npc);
        }
    }

    private void MoveNPCToEnd(GameObject npc)
    {
        npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[waypoints.Length - 1].position);
    }

    private void NPCOrderStatus()
    {
        if (npcList != null)
        {
            _countStatus = 0;
            npcList.ForEach(x =>
            {
                if (x.OrderStatus == true)
                {
                    _countStatus++;
                }
            });

            if (_countStatus >= 3)
            {
                StopCoroutine(SpawnNPC());
                _spawnMax = true;
            }
            if (_countStatus < 3 && _spawnMax == true)
            {
                StartCoroutine(SpawnNPC());
                _spawnMax = false;
            }
        }

    }

    private void CheckCollisionNPC(GameObject npc, bool status, int npcNumber, bool orderStatus)
    {
        NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();

        Collider[] colliders = Physics.OverlapBox(npc.transform.position, npc.transform.localScale / 1.0f,
            Quaternion.identity, LayerMask.GetMask("npc"));
        foreach (Collider hitCollider in colliders)
        {
            if (hitCollider.gameObject != npc.gameObject && orderStatus == true)
            {

                // Stop the NavMeshAgent of the current NPC if another NPC is in the environment
                navMeshAgent.isStopped = true;

                return;
            }
        }

        // If no other NPC is around, continue the NavMeshAgent
        navMeshAgent.isStopped = false;

    }
    IEnumerator SpawnNPC()
    {
        while (true)
        {
            //Npc no longer spawn if 3 Npcs exist in the current scene
            if (_countStatus >= 3)
            {
                break;
            }

            yield return new WaitForSeconds(spawnTime); // Wait for a certain time
            GameObject npc = Instantiate(npcPrefab, _spawnPosition, Quaternion.identity); // Create a new NPC at the position of the last NPC
            if (npc != null)
            {
                _count++;
                npc.name = ("Customer " + _count);
                npc.tag = "npc";
                npcList.Add(new Npc
                {
                    ID = _count,
                    Object = npc,
                    Name = npc.name,
                    Order = orderClass.GenerateOrder(),
                    OrderStatus = true
                });
                

                npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position); //After spawning move to the bar
                yield return null;
            }

        }


    }

}
