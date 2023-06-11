using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    const int MAX_CUSTOMERS = 3;
    [SerializeField]
    const float SPAWN_DELAY = 5f;
    [SerializeField]
    const string CUSTOMER_TAG = "Customer";

    public LinkedList<GameObject> customers = new LinkedList<GameObject>();
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
        // * check if any customer is close to the endpoint and destroy it
        foreach (GameObject customer in customers)
        {
            if (customer == null) continue;
            if (Vector3.Distance(customer.transform.position, waypointEnd.position) < 1.5f)
            {
                Destroy(customer);
                customers.Remove(customer);
                nCustomers--;
                Debug.Log("Customer destroyed");
                break;
            }
        }

        if (!status.Equals(LiveCycleStatus.Active)) return;
        if (nCustomers >= MAX_CUSTOMERS) return;
        if (timeManager.CurrentTime() - tLastSpawn < SPAWN_DELAY) return;

        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, waypointStart.position, Quaternion.identity);
        customer.tag = CUSTOMER_TAG;
        customer.name = "Customer_" + nTotalCustomers;
        customer.GetComponent<Customer>().Init(customer);
        customers.AddLast(customer);

        tLastSpawn = timeManager.CurrentTime();
        nTotalCustomers++;
        nCustomers++;
        Debug.Log("Customer spawned");
    }

    public void Reset() {
        customers.Clear();
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