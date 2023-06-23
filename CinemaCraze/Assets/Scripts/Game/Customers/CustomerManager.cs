using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Settings")]
    [Range(1, 10)]
    public int MAX_NUMBER_OF_CUSTOMERS = 3;
    [Range(1, 10)]
    public float SPAWN_DELAY = 5f;

    const string CUSTOMER_TAG = "Customer";

    public List<GameObject> customers = new List<GameObject>();
    public int customerCount;
    public int totalCustomerCount;
    public float timeOfLastSpawn;
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

        SpawnRoutine();

        CustomerRoutine();



        // * check if any customer is close to the endpoint and destroy it
        foreach (GameObject customer in customers)
        {
            if (customer == null) continue;
            if (Vector3.Distance(customer.transform.position, waypointEnd.position) < 1.5f)
                DestroyCustomer(customer);
        }

    }

    void SpawnRoutine()
    {
        if (customerCount >= MAX_NUMBER_OF_CUSTOMERS) return;
        if ((Time.time - timeOfLastSpawn) < SPAWN_DELAY) return;

        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, waypointStart.position, Quaternion.identity);
        long id = totalCustomerCount;

        customer.tag = "Customer";
        customer.name = "Customer_" + id;



        customer.GetComponent<Customer>().Init(customer);
        customers.Add(customer);

        timeOfLastSpawn = timeManager.CurrentTime();
        totalCustomerCount++;
        customerCount++;
        Debug.Log("Customer spawned");
    }

    void CustomerRoutine()
    {
        foreach (GameObject customer in customers)
        {
            
        }
    }

    void DestroyCustomer(GameObject customer)
    {
        try
        {
            customers.Remove(customer);
            Destroy(customer);
            customerCount--;
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
        customerCount = 0;
        totalCustomerCount = 0;
        timeOfLastSpawn = -5f;

        status = LiveCycleStatus.Inactive;
        
    }

}

public enum LiveCycleStatus
{
    Inactive,
    Active,
    Paused
}