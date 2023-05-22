
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TimeScript : MonoBehaviour
{

    public NPC npc;
    public Text gametimeText;
    public float basetime = 30.0f;
    public float popcornTime = 3.0f;
    public float nachoTime = 4.0f;
    public string timeBorder1 = "00:30";
    public string timeBorder2 = "01:00";
    public string timeBorder3 = "01:30";
    float orderTime;
    float _gametime;
    int _minuten;
    int _sekunden;
    float _generatedTime = 10.0f;

   
    // Update is called once per frame
    void Update()
    {
       
        _gametime = Time.time;
        _minuten = Mathf.FloorToInt(_gametime / 60f);
        _sekunden = Mathf.FloorToInt(_gametime % 60f);
        gametimeText.text = _minuten.ToString("00") + ":" + _sekunden.ToString("00");

        if (gametimeText.text == timeBorder1)
        {
            basetime = 20.0f;
        }
        if (gametimeText.text == timeBorder2)
        {
            basetime = 10.0f;
        }
        if (gametimeText.text == timeBorder3)
        {
            basetime = 0.0f;
        }

    }

    public float generateWaitingTime(GameObject npcInOrder)
    {
        orderTime = 0;
        for(int i = 0; i < npc.npcList.Count; i++)
        {
            if (npcInOrder.name == npc.npcList[i].Name)
            {
                List<string> listOrder = new List<string>();
                listOrder = npc.npcList[i].Order;
                for (int j = 0; j < listOrder.Count; j++)
                {
                    if (listOrder[j] != null)
                    {
                        if (listOrder[j].Contains("Popcorn"))
                        {
                            string num = Regex.Match(listOrder[j], @"\d+").Value;
                            orderTime += Int32.Parse(num) * popcornTime;
                        }
                        if (listOrder[j].Contains("Nacho"))
                        {
                            string num = Regex.Match(listOrder[j], @"\d+").Value;
                            orderTime += Int32.Parse(num) * nachoTime;
                        }
                    }
                }
                
            }
        }
        
        _generatedTime = basetime + orderTime;  
        return _generatedTime;
    }
}
