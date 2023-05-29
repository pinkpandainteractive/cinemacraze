using UnityEngine;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{

    const int MAX_CUSTOMERS = 3;
    const float SPAWN_DELAY = 5f;
    public GameObject customerPrefab;
    int numberOfCustomers = 0;
    int customerCount = 0;
    float lastSpawnTime = 0f;
    bool active = true;
    Vector3 spawnPoint;

    // * Waypoints
    public Transform _waypoint_start;
    public Transform _waypoint_bar;
    public Transform _waypoint_end;

    public void StartLiveCycle()
    {
        active = true;
    }

    public void StopLiveCycle()
    {
        active = false;
    }

    void Update()
    {
        if (!active) return;
        if (numberOfCustomers >= MAX_CUSTOMERS) return;
        if (Time.time - lastSpawnTime < SPAWN_DELAY) return;

        GameObject customer = Instantiate(customerPrefab, _waypoint_start.position, Quaternion.identity);
        customer.name = "Customer_" + customerCount;
        customer.tag = "Customer";
        customer.GetComponent<Customer>().orderStatus = OrderStatus.None;
        customer.GetComponent<Customer>().movementStatus = MovementStatus.Idle;
        customer.GetComponent<NavMeshAgent>().SetDestination(_waypoint_bar.position);
        customer.GetComponent<Customer>().customer = customer;

        lastSpawnTime = Time.time;
        customerCount++;
        numberOfCustomers++;
    }

}