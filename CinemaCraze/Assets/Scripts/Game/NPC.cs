using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public GameObject npcPrefab; // Prefab for the npc
    public List<GameObject> npcList;
    public List<bool> npcStatusList;
    public float spawnTime = 5f;

    private Vector3 spawnPosition;
    private int count = 0;
    private int countStatus = 0;
    private bool checkStatus = false;
    public ObjectSelector objectSelector;
    private void Start()
    {
        //objectSelector = GetComponent<ObjectSelector>();

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
                if (clickedObject != null && clickedObject.name.Contains("Customer"))
                {
                    /*if (objectSelector != null)
                    {
                        string text = objectSelector.selectedObjectText.text;
                        Debug.Log(text);
                        if (text.Contains("Popcorn"))
                        {*/
                            MoveNPCToEnd(clickedObject);
                       // }
                    //}
                    // MoveNPCToEnd(clickedObject);
                }
            }
        }

        //Go through all npc and see if they need to be deleted or collide with each other
        if (npcList != null)
        {
            for (int i = 0; i < npcList.Count; i++)
            {
                if (npcList[i] != null)
                {
                    CheckCollisionNPC(npcList[i], npcList[i].transform.position, waypoints[0].position, npcStatusList[i]);
                    DestroyNPC(npcList[i], npcList[i].transform.position, waypoints[waypoints.Length - 1].position);
                }

            }

        }
        //Status for npc; true means the maximum of npcs exists on the scene (3)
        checkStatus = NPCStatus();


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
        string getNumber = npc.name.Substring(8);
        int num = int.Parse(getNumber);
        npcStatusList[num - 1] = false;
    }

    private bool NPCStatus()
    {
        countStatus = 0;
        npcStatusList.ForEach(x =>
        {
            if (x == true)
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
        return false;
    }

    private void CheckCollisionNPC(GameObject npc, Vector3 npcPosition, Vector3 endTarget, bool status)
    {

        // Überprüfung, ob sich ein anderes GameObject mit dem Tag "NPC" in der Nähe befindet
        
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
           // npc.tag = "NPC";
            count++;
            npc.name = ("Customer " + count);
            npcList.Add(npc);
            npcStatusList.Add(true);


            if (npc != null)
            {
                npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position);
                yield return null;
            }

        }


    }
}
