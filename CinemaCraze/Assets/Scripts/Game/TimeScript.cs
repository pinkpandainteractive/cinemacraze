
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;


public class TimeScript : MonoBehaviour
{

    
    public Text gametimeText;
    public float basetimeGeneral = 0.0f;
    public float popcornTime = 3.0f;
    public float nachoTime = 4.0f;
    public string timeBorder1 = "00:30";
    public float basetimeBorder1 = 0.0f;
    public string timeBorder2 = "01:00";
    public float basetimeBorder2 = 0.0f;
    public string timeBorder3 = "01:30";
    public float basetimeBorder3 = 0.0f;

    float _orderTime;
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
            basetimeGeneral = basetimeBorder1;
        }
        if (gametimeText.text == timeBorder2)
        {
            basetimeGeneral = basetimeBorder2;
        }
        if (gametimeText.text == timeBorder3)
        {
            basetimeGeneral = basetimeBorder3;
        }

    }

    public float generateWaitingTime(GameObject npcInOrder)
    {
        _orderTime = 0;
        
        List<string> listOrder = npcInOrder.GetComponent<CustomComponent>().Order;
        for (int j = 0; j < listOrder.Count; j++)
        {
            if (listOrder[j] != null)
            {
                if (listOrder[j].Contains("Popcorn"))
                {
                    string num = Regex.Match(listOrder[j], @"\d+").Value;
                    _orderTime += Int32.Parse(num) * popcornTime;
                }
                if (listOrder[j].Contains("Nacho"))
                {
                    string num = Regex.Match(listOrder[j], @"\d+").Value;
                    _orderTime += Int32.Parse(num) * nachoTime;
                }
            }   
        }

        _generatedTime = basetimeGeneral + _orderTime;  
        return _generatedTime;
    }
}
