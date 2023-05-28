using UnityEngine;

public class NPCDestroy : MonoBehaviour
{
    public Transform waypoint;

    // Update is called once per frame
    void Update()
    {
        DestroyNPC(gameObject, gameObject.transform.position, waypoint.position);
    }
    private void DestroyNPC(GameObject npc, Vector3 positionNPC, Vector3 endTarget)
    {
        if (Vector3.Distance(positionNPC, endTarget) < 2.5f && npc != null)
        {
            Destroy(npc);
        }
    }
}
