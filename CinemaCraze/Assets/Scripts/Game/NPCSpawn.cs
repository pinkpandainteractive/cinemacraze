using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawn : MonoBehaviour
{
    
    public int countStatus = 0;
    public NPC npcs;
    public float spawnTime = 5f;
    public GameObject npcPrefab; // Prefab for the npc
    public Transform waypoint;

    private Vector3 _spawnPosition;
    private int _count = 0;
    private bool _maxSpawn = false;

    void Start()
    {
        _spawnPosition = new Vector3(-399.7f, 1.3f, -14.0f); //Spawn position npc
        StartCoroutine(SpawnNPC());
    }

    // Update is called once per frame
    void Update()
    {
        if(countStatus < 3 && _maxSpawn == true)
        {
            StartCoroutine(SpawnNPC());
        }
    }
    public IEnumerator SpawnNPC()
    {
        while (true)
        {
            _maxSpawn = false;
            //Npc no longer spawn if 3 Npcs exist in the current scene
            if (countStatus >= 3)
            {
                _maxSpawn = true;
                break;
            }
            countStatus += 1;
            yield return new WaitForSeconds(spawnTime); // Wait for a certain time
            GameObject npc = Instantiate(npcPrefab, _spawnPosition, Quaternion.identity); // Create a new NPC at the position of the last NPC
            if (npc != null)
            {
                _count++;
                npc.name = ("Customer " + _count);
                npc.tag = "npc";
                npc.GetComponent<NavMeshAgent>().SetDestination(waypoint.position); //After spawning move to the bar
                yield return null;
            }
        }
    }
}
