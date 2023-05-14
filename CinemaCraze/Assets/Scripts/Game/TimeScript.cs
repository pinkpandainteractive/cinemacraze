using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class TimeScript : MonoBehaviour
{

    public NPC npc;
    public GameObject npcObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Text spielzeitText;
    // Update is called once per frame
    void Update()
    {
        float spielzeit = Time.time;
        int minuten = Mathf.FloorToInt(spielzeit / 60f);
        int sekunden = Mathf.FloorToInt(spielzeit % 60f);
        spielzeitText.text = "Spielzeit: " + minuten.ToString("00") + ":" + sekunden.ToString("00");
        
        
       
        for (int j = 0; j < npc.npcList.Count; j++)
        {
           
            if (npc.npcList[j].WaitingStatus == true)
            {
                
            }

        }
    }
}
