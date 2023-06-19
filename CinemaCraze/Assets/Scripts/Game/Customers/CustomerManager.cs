using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Settings")]
    [Range(1, 10)]
    public int MAX_CUSTOMERS = 3;
    [Range(1, 10)]
    public float SPAWN_DELAY = 5f;

    const string CUSTOMER_TAG = "Customer";

    public List<GameObject> customers = new List<GameObject>();
    int nCustomers;
    int nTotalCustomers;
    float tLastSpawn;
    public LiveCycleStatus status { get; set; }

    public TimeManager timeManager;
    public GameObject customerPrefab;

    // * Waypoints
    public Transform waypointStart;
    public Transform waypointBar;
    public Transform waypointBeforeEnd;
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
                DestroyCustomer(customer);
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
        customers.Add(customer);

        tLastSpawn = timeManager.CurrentTime();
        nTotalCustomers++;
        nCustomers++;
        Debug.Log("Customer spawned");
    }

    void DestroyCustomer(GameObject customer)
    {
        try
        {
            customers.Remove(customer);
            Destroy(customer);
            nCustomers--;
            Debug.Log("Customer destroyed");
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }

    public void Reset()
    {
        foreach (GameObject customer in customers)
        {
            if (customer == null) continue;
            Destroy(customer);
        }
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