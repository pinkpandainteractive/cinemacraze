using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static NPC;

public class NPCCanvas : MonoBehaviour
{
    public GameObject npcObject;
    public NPC npc;
    public ZoneScript zone;
    public Text canvasText;
    private Canvas _canvas;

    void Start()
    {
        // Accessing the Canvas Component
        _canvas = GetComponent<Canvas>();
        if(npc.npcList.Count > 0 )
        {
            for(int i = 0; i < npc.npcList.Count; i++)
            {
                if (npc.npcList[i] != null)
                {
                    if (npc.npcList[i].Name == npcObject.name)
                    {
                        // Creates the order text for the customer
                        List<string> listOrder = npc.npcList[i].Order;
                        canvasText.text = "";
                        for (int j = 0; j < listOrder.Count; j++)
                        {
                            canvasText.text += listOrder[j];
                            if (j + 1 < listOrder.Count)
                            {
                                canvasText.text += ", ";
                            }

                        }
                    } 
                }
            }
        }
        
        
    }

    void Update()
    {
        // Sets the position of the canvas GameObject to the position of the NPC GameObject
        _canvas.transform.position = npcObject.transform.position;

        //canvas.transform.Translate(Vector3.up);
        // Moves the Canvas GameObject in the local Z-axis
        _canvas.transform.Translate(Vector3.back);
    }
}