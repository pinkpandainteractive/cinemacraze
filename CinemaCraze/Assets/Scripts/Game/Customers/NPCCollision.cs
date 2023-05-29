using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPCCollision : MonoBehaviour
{

    public Order orderClass;
   
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CustomComponent>().OrderStatus = true;
        gameObject.GetComponent<CustomComponent>().Order = orderClass.GenerateOrder();
    }

    // Update is called once per frame
    void Update()
    {     
        CheckCollisionNPCinBar(gameObject, gameObject.GetComponent<CustomComponent>().OrderStatus);
    }
    

    private void CheckCollisionNPCinBar(GameObject npc, bool orderStatus)
    {
        NavMeshAgent navMeshAgent = npc.GetComponent<NavMeshAgent>();
        if (orderStatus == true)
        {
            
            Collider[] colliders = Physics.OverlapBox(npc.transform.position, npc.transform.localScale / 1.0f,
                Quaternion.identity, LayerMask.GetMask("npc"));

            Vector3 npcForward = npc.transform.forward; // Richtung des NPCs in lokalen Koordinaten
            foreach (Collider hitCollider in colliders)
            {
                if (hitCollider.gameObject != npc)
                {
                    // Richtung zum Kollisionsobjekt berechnen
                    Vector3 directionToCollider = hitCollider.transform.position - npc.transform.position;

                    // Überprüfen, ob die Kollision in positive Z-Richtung liegt
                    if (Vector3.Dot(directionToCollider, npcForward) > 0)
                    {
                        // Stoppe den NavMeshAgent des aktuellen NPCs, wenn ein anderer NPC in der Umgebung ist
                        navMeshAgent.isStopped = true;
                        return;
                    }
                }
            }
        }
            // Wenn kein anderer NPC in der Nähe ist, setze den NavMeshAgent fort
            navMeshAgent.isStopped = false;
       
    }
}
