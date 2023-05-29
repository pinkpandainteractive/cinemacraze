using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // Creates the order text for the customer
        List<string> listOrder = npcObject.GetComponent<CustomComponent>().Order;
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

    void Update()
    {
        // Sets the position of the canvas GameObject to the position of the NPC GameObject
        _canvas.transform.position = npcObject.transform.position;

        
        // Moves the Canvas GameObject in the local Z-axis
        _canvas.transform.Translate(Vector3.back);
    }
}