using UnityEngine;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    const int MAX_CUSTOMERS = 3;
    [SerializeField]
    const float SPAWN_DELAY = 5f;

    int nCustomers;
    int nTotalCustomers;
    float tLastSpawn;
    public LiveCycleStatus status { get; set; }

    public TimeManager timeManager;
    public GameObject customerPrefab;

    // * Waypoints
    public Transform waypointStart;
    public Transform waypointBar;
    public Transform waypointEnd;

    void Start()
    {
        Reset();
    }
    void Update()
    {
        if (!status.Equals(LiveCycleStatus.Active)) return;
        if (nCustomers >= MAX_CUSTOMERS) return;
        if (timeManager.CurrentTime() - tLastSpawn < SPAWN_DELAY) return;

        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        tLastSpawn = timeManager.CurrentTime();
        nTotalCustomers++;
        nCustomers++;

        GameObject customer = Instantiate(customerPrefab, waypointStart.position, Quaternion.identity);
        customer.name = "Customer_" + nTotalCustomers;
        customer.tag = "Customer";

        customer.GetComponent<Customer>().orderStatus = OrderStatus.None;
        customer.GetComponent<Customer>().movementStatus = MovementStatus.Idle;
        customer.GetComponent<NavMeshAgent>().SetDestination(waypointBar.position);
        customer.GetComponent<Customer>().customer = customer;

        Debug.Log("Customer spawned");
    }

    public void Reset() {
        nCustomers = 0;
        nTotalCustomers = 0;
        tLastSpawn = -5f;
        status = LiveCycleStatus.Inactive;
    }

}

public enum LiveCycleStatus
{
    Inactive,
    Active,
    Paused
}