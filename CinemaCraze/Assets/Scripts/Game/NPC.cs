using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static NPC;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public GameObject npcPrefab; // Prefab for the npc
    public GameObject zone;


    public List<Npc> npcList = new List<Npc>();
    public List<string> npcOrder;
    public float spawnTime = 5f;
    public ObjectSelector objectSelector;
    public MainMenu mainMenu;
    public Order orderClass;
    
    private Vector3 spawnPosition;
    private int count = 0;
    private int countStatus = 0;
    private bool checkStatus = false;
    
    public class Npc
    {
        public int ID { get; set; }
        public GameObject Object { get; set; }
        public string Name { get; set; }
        public List<string> Order { get; set; }
        //OrderStatus = true means that the order has not yet been processed
        public bool OrderStatus { get; set; }
        public bool WaitingStatus { get; set; }
    }


    private void Start()
    {
        spawnPosition = new Vector3(-399.7f, 1.3f, -14.0f); //Spawn position npc
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
                    npcList.ForEach(x =>
                    {
                    if (x.Name == clickedObject.name)
                    {
                        if (CheckMatch(npcList[x.ID - 1].Order, objectSelector.listSelectedObjects) == true)
                    {
                        MoveNPCToEnd(clickedObject);
                        objectSelector.Delete();
                        npcList[x.ID - 1].OrderStatus = false;
                    }
                    }
                    });
                }
            }
        }

        //Go through all npc and see if they need to be deleted or collide with each other
        if (npcList != null )
        {
            for (int i = 0; i < npcList.Count; i++)
            {
                if (npcList[i].Object != null)
                {
                    CheckCollisionNPC(npcList[i].Object, npcList[i].OrderStatus, i);
                    DestroyNPC(npcList[i].Object, npcList[i].Object.transform.position, waypoints[waypoints.Length - 1].position);
                }
            }
        }

        // Prüfe, ob sich der NPC innerhalb der Wartezone befindet
        Collider[] colliders = Physics.OverlapBox(zone.transform.position, zone.transform.localScale / 2f);
        float speed = -1;
        bool isInZone = false;
        foreach (Collider col in colliders)
        {
            
            if (col.gameObject.tag == "npc")
            {
                speed = col.GetComponent<NavMeshAgent>().velocity.magnitude;
                isInZone = true;
                break;
            }
        }
        if (isInZone && speed == 0)
        {
            Debug.Log("NPC befindet sich innerhalb der Wartezone und steht still!");
        }
        

        //Status for npc; true means the maximum of npcs exists on the scene (3)
        checkStatus = NPCStatus();


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
                if (x.OrderStatus == true)
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

    private void CheckCollisionNPC(GameObject npc, bool status, int npcNumber)
    {
        NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();


        float speed = navMeshAgent.velocity.magnitude;

        
       

        //Debug.Log("NPC " + targetPosition + " Ziel "+ waypoints[0].position);
        if (speed == 0 )
        {
            npcList[npcNumber].WaitingStatus = true;
        }
        else
        {
            npcList[npcNumber].WaitingStatus = false;
        }
        Vector3 collisionDirection = new Vector3(0, 0, -1); // negative Z-Richtung

        
        Collider[] colliders = Physics.OverlapBox(npc.transform.position, npc.transform.localScale / 1.0f, 
            Quaternion.identity, LayerMask.GetMask("npc"));
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
            if (npc != null)
            {
            count++;
            npc.name = ("Customer " + count);
            npc.tag = "npc";
            npcList.Add(new Npc
            {
                ID = count,
                Object = npc,
                Name = npc.name,
                Order = orderClass.GenerateOrder(),
                OrderStatus = true,
                WaitingStatus = false,
            });
                //Pass global parameters. Important for NPCCanvas Script
                if (npcList.Count > 0)
                {
                   
                    npcOrder = npcList[count - 1].Order;
                }
                
                npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position); //After spawning move to the bar
                yield return null;
            }

        }


    }

}
