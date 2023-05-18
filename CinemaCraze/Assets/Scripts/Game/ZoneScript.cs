using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class ZoneScript : MonoBehaviour
{
    public float waitTime = 10f;
    public Transform[] waypoints;
    public TimeScript timeScript;
    public NPC npcList;
    private readonly Dictionary<GameObject, float> _npcTimers = new();
    private bool _inZone = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("npc"))
        {
            GameObject npc = other.gameObject;
            _npcTimers.Add(npc, timeScript.generateWaitingTime());
            _inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("npc"))
        {
            GameObject npc = other.gameObject;
            _npcTimers.Remove(npc);
            Destroy(npc.transform.GetChild(0).gameObject);

            if (_npcTimers.Count == 0)
            {
                _inZone = false;
            }
        }
    }

    private void Update()
    {
        if (_inZone)
        {
            // Erstelle eine separate Liste von NPCs zum Iterieren
            List<GameObject> npcsToIterate = new List<GameObject>(_npcTimers.Keys);

            foreach (GameObject npc in npcsToIterate)
            {
                NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();

                
                float speed = navMeshAgent.velocity.magnitude;
                if(speed == 0.0f)
                {
                
                    if (_npcTimers[npc] <= 0f)
                    {
                        // Time's up, move NPC to next waypoint
                        npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position);
                        npcList.npcList.ForEach(x =>
                        {
                            if (npc.name == x.Name)
                            {
                                x.OrderStatus = false;
                            }
                        });
                     
                    }
                    else
                    {
                        // Still waiting, count down timer
                        _npcTimers[npc] -= Time.deltaTime;

                        // Aktualisiere den Timer-Text
                        Text timerTextComponent = npc.transform.GetChild(0).GetComponentInChildren<Text>();
                        timerTextComponent.text = _npcTimers[npc].ToString("F1");
                    }
                }
                
            }
        }
    }

}