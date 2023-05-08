using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
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
    public Text order;

    private Vector3 spawnPosition;
    private int count = 0;
    private int countStatus = 0;
    private bool checkStatus = false;
    public ObjectSelector objectSelector;
    public MainMenu mainMenu;
    public Order orderClass;

    private GameObject currentCustomerOrder;
    private int countForStatusOrder = 0;
    List<string> listOrder = new List<string>();

    public class Npc
    {
        public int ID { get; set; }
        public GameObject Object { get; set; }
        public string Name { get; set; }
        public List<string> Order { get; set; }
        public bool Status { get; set; }
    }


    private void Start()
    {
        spawnPosition = new Vector3(-400.0f, 1.3f, -14.0f); //Spawn position npc
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
                if (clickedObject != null && clickedObject.name.Contains("Customer") && listOrder!= null)
                {
                    npcList.ForEach(x =>
                    {
                    if (x.Name == clickedObject.name)
                    {
                        if (CheckMatch(npcList[x.ID - 1].Order, objectSelector.listSelectedObjects) == true)
                    {
                        MoveNPCToEnd(clickedObject);
                        objectSelector.Delete();
                        orderClass.DeleteOrder(listOrder);
                        npcList[x.ID - 1].Status = false;
                    }
                    }
                    });
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
                    CheckCollisionNPC(npcList[i].Object, npcList[i].Object.transform.position, waypoints[0].position, npcList[i].Status);
                    DestroyNPC(npcList[i].Object, npcList[i].Object.transform.position, waypoints[waypoints.Length - 1].position);

                    if (Vector3.Distance(npcList[i].Object.transform.position, waypoints[0].position) < 1.0f && countForStatusOrder == 0)
                    {
                        currentCustomerOrder = npcList[i].Object;
                        countForStatusOrder++;
                        listOrder = npcList[i].Order;
                    }
                    if (currentCustomerOrder != null)
                    {
                        if (Vector3.Distance(currentCustomerOrder.transform.position, waypoints[0].position) > 1.0f)
                        {
                            countForStatusOrder = 0;
                        }
                    }

                }

            }

        }
        //Status for npc; true means the maximum of npcs exists on the scene (3)
        checkStatus = NPCStatus();


    }

    bool CheckMatch(List<string> l1, List<string> l2)
    {
        
        if (l1.Count == 0 || l2.Count == 0)
        {
            return false;
        }
        Debug.Log("l1 " + l1[0] + "l2 " + l2[0]);
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

        return true;
    }

    private bool DestroyNPC(GameObject npc, Vector3 positionNPC, Vector3 endTarget)
    {

        if (Vector3.Distance(positionNPC, endTarget) < 2.5f && npc != null)
        {
            Debug.Log("Destroy npc: " + npc.name);
            Destroy(npc);
            return true;
        }

        return false;

    }

    private void MoveNPCToEnd(GameObject npc)
    {
        npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[waypoints.Length - 1].position);
    }

    private bool NPCStatus()
    {
        if (npcList != null)
        {
            countStatus = 0;
            npcList.ForEach(x =>
            {
                if (x.Status == true)
                {
                    countStatus++;
                }
            });

            if (countStatus >= 3)
            {

                StopCoroutine(SpawnNPC());
                checkStatus = true;
                return true;
            }
            if (countStatus < 3 && checkStatus == true)
            {

                StartCoroutine(SpawnNPC());
                checkStatus = false;
                return false;
            }
        }
        return false;
    }

    private void CheckCollisionNPC(GameObject npc, Vector3 npcPosition, Vector3 endTarget, bool status)
    {
        NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();
        Vector3 collisionDirection = new Vector3(0, 0, -1); // negative Z-Richtung
        Collider[] colliders = Physics.OverlapSphere(npc.transform.position, 1.5f, LayerMask.GetMask("npc"));
        foreach (Collider hitCollider in colliders)
        {
            Vector3 directionToCollider = (hitCollider.transform.position - npc.transform.position).normalized;
            
            if (Vector3.Dot(directionToCollider, collisionDirection) > 0)
            {
                //Ignore collision on the wrong diraction
                continue;
            }
           
            if (Vector3.Dot(directionToCollider, collisionDirection) < 0 && status == true)
            {

                //Stop the NavMeshAgent of the current NPC if another NPC is in the surrounding area
                navMeshAgent.isStopped = true;
                return;
            }
        }
        // If no other NPC is nearby, continue the NavMeshAgent
        navMeshAgent.isStopped = false;
    }
    IEnumerator SpawnNPC()
    {
        while (true)
        {
            //Npc no longer spawn if 3 Npcs exist in the current scene
            if (countStatus >= 3)
            {
                break;
            }

            yield return new WaitForSeconds(spawnTime); // Wait for a certain time
            GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity); // Create a new NPC at the position of the last NPC
            count++;
            if (npc != null)
            {
            npc.name = ("Customer " + count);

            npcList.Add(new Npc
            {
                ID = count,
                Object = npc,
                Name = npc.name,
                Order = orderClass.GenerateOrder(),
                Status = true
            });
           
                npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position);
                yield return null;
            }

        }


    }

}
