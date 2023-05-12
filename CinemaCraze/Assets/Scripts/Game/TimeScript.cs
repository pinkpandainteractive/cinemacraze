using UnityEngine;
using UnityEngine.UI;
public class TimeScript : MonoBehaviour
{
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
    }
}
