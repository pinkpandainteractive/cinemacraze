
using UnityEngine;
using UnityEngine.UI;


public class TimeScript : MonoBehaviour
{

    public NPC npc;
    public Text gametimeText;
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

        string border1 = "00:10";
        string border2 = "00:30";
        string border3 = "01:00";

        if (gametimeText.text == border1)
        {
            _generatedTime = 8.0f;
        }
        if (gametimeText.text == border2)
        {
            _generatedTime = 6.0f;
        }
        if (gametimeText.text == border3)
        {
            _generatedTime = 4.0f;
        }
        
    }

    public float generateWaitingTime()
    {
        return _generatedTime;
    }
}
