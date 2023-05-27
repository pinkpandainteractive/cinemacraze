using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class NPC : MonoBehaviour
{

    public Score score;
    public Camera CameraMain;
    public Camera CameraProduct;
    public CameraSwitch CameraSwitch;
    public NavMeshAgent agent;
    public Transform waypointEnd;
    public GameObject npcPrefab; // Prefab for the npc
    public float spawnTime = 5f;
    public ObjectSelector objectSelector;
    public Order orderClass;
    public NPCSpawn npcSpawn;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray;
            if (CameraSwitch.isCameraMainActive == true)
            {
                ray = CameraMain.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = CameraProduct.ScreenPointToRay(Input.mousePosition);
            }
            int layerMask = ~LayerMask.GetMask("WaitingZone"); //Ignore WaitingZone

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                //Define clickedObject
                GameObject clickedObject = hit.collider.gameObject;

                //If you click on the npc, it moves to the next waypoint (waypoint end)
                if (clickedObject != null && clickedObject.name.Contains("Customer")
                    && objectSelector.listSelectedObjects != null)
                {

                    if (CheckMatch(clickedObject.GetComponent<CustomComponent>().Order, objectSelector.listSelectedObjects) == true)
                    {
                        Debug.Log("Order is correct");
                        MoveNPCToEnd(clickedObject);
                        objectSelector.Delete();
                        clickedObject.GetComponent<CustomComponent>().OrderStatus = false;
                        npcSpawn.countStatus -= 1;
                        
                        //Add 100 points to the score
                        score.AddScore(100);

                    }
                }
            }
        }
     
    }

   

    private void MoveNPCToEnd(GameObject npc)
    {
        npc.GetComponent<NavMeshAgent>().SetDestination(waypointEnd.position);
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

}
