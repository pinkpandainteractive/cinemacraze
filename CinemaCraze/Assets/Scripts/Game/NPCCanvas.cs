using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class NPCCanvas : MonoBehaviour
{
    public GameObject npcObject;
    public NPC npc;
    public Text canvasText;
    private Canvas canvas;

    void Start()
    {
        // Zugriff auf das Canvas-Component
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        // Aktualisiert den Text im Canvas
        if(npc.npcName == npcObject.name)
        {
            List<string> listOrder = npc.npcOrder;
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
       
       
        // Setzt die Position des Canvas-GameObjects an die Position des NPC-GameObjects
        canvas.transform.position = npcObject.transform.position;

        // Bewegt das Canvas-GameObject in der lokalen Z-Achse
        canvas.transform.Translate(Vector3.up);
        canvas.transform.Translate(Vector3.back);
    }
}