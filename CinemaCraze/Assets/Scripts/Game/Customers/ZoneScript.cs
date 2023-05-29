using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class ZoneScript : MonoBehaviour
{

    public GameObject _gameOverScreen;
    public GameObject _gameOverlayUI;


    public Lives lives;

    public float waitTime = 10f;
    public Transform[] waypoints;
    public NPC npcs;
    public TimeScript timeScript;
    public NPCSpawn npcSpawn;
    public NPC npcList;

    private readonly Dictionary<GameObject, float> _npcTimers = new();
    private bool _inZone = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("npc"))
        {
            GameObject npc = other.gameObject;
            _npcTimers.Add(npc, timeScript.generateWaitingTime(npc));
            _inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("npc"))
        {
            GameObject npc = other.gameObject;
            _npcTimers.Remove(npc);
            Destroy(npc.transform.GetChild(0).gameObject); // TODO manchmal fliegt hier ne 'OutOfBoundsException'

            if (_npcTimers.Count == 0)
            {
                _inZone = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_inZone)
        {
            // Erstelle eine separate Liste von NPCs zum Iterieren
            List<GameObject> npcsToIterate = new List<GameObject>(_npcTimers.Keys);
            foreach (GameObject npc in npcsToIterate)
            {
                NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();

                float speed = navMeshAgent.velocity.magnitude;
                if (speed == 0.0f)
                {

                    if (_npcTimers[npc] <= 0f)
                    {
                        // Time's up, move NPC to next waypoint
                        npc.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position);
                        npc.GetComponent<CustomComponent>().OrderStatus = false;
                        npcSpawn.countStatus -= 1;
                        
                        lives.LoseLife();
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