using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Settings")]
    [Range(1, 10)]
    public int MAX_NUMBER_OF_CUSTOMERS = 3;
    [Range(1, 10)]
    public float SPAWN_DELAY = 5f;

    const string CUSTOMER_TAG = "Customer";
    const float ROTATION_TIME = 1.0f;


    public List<GameObject> customers = new List<GameObject>();
    public int customerCount;
    public int totalCustomerCount;
    public float timeOfLastSpawn;
    public LiveCycleStatus status { get; set; }

   // public TimeManager timeManager;
    public GameObject customerPrefab;
    public Camera player;

    // * Waypoints
    public Transform waypointStart;
    public Transform waypointBar;
    public Transform waypointBeforeEnd;
    public Transform waypointEnd;

    public bool gameRunning = false;

    void Start()
    {
        Reset();
    }
    void Update()
    {
        if (!gameRunning) return;

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

        customers.Add(SpawnCustomer());
    }

    GameObject SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, waypointStart.position, Quaternion.identity);

        long id = totalCustomerCount;
        string name = "Customer_" + id;

        customer.tag = "Customer";
        customer.name = name;
        customer.GetComponent<CustomerLogic>().Initialize(name, id, waypointStart.position, waypointStart.forward, waypointBar.position);

        // !old line
        customer.GetComponent<Customer>().Init(customer);

        timeOfLastSpawn = Time.time;
        totalCustomerCount++;
        customerCount++;

        Debug.Log("Customer spawned");

        return customer;
    }

    // TODO sachen auslagern is eigene Methoden
    void CustomerRoutine()
    {
        foreach (GameObject customer in customers)
        {
            CustomerLogic logic = customer.GetComponent<CustomerLogic>();
            logic.UpdatePosition(customer.transform);

            OrderStatus orderStatus = logic.GetOrderStatus();
            MovementStatus movementStatus = logic.GetMovementStatus();

            if (orderStatus.Equals(OrderStatus.Undefined))
            {
                logic.GenerateOrder();
            }
            else if (orderStatus.Equals(OrderStatus.Ordering))
            {
                logic.UpdateOrderText();
                if (movementStatus.Equals(MovementStatus.MovingToBar))
                {
                    if (logic.GetDistanceToDestination() < 0.5f)
                    {
                        logic.SetMovementStatus(MovementStatus.IdleAtBar);
                    }
                    else
                    {
                        if(logic.GetDistanceToDestination() < 6.0f)
                            StartCoroutine(RotateCustomer(customer, RotationStatus.RotatedTowardsPlayer));
                        logic.KeepDistanceToOtherCustomers();
                    }
                }
                else if (movementStatus.Equals(MovementStatus.IdleAtBar))
                {
                    if (logic.GetDistanceToDestination() > 0.5f)
                    {
                        logic.KeepDistanceToOtherCustomers();
                    }
                }
            }
            else if (orderStatus.Equals(OrderStatus.Completed) || orderStatus.Equals(OrderStatus.Failed))
            {
                if (movementStatus.Equals(MovementStatus.IdleAtBar))
                {
                    Debug.Log(logic.data.getName() + " is moving to before end");

                    // TODO Rotation towards the before end waypoint
                    logic.SetDestination(waypointBeforeEnd.position);
                    logic.SetMovementStatus(MovementStatus.MovingToBeforeEnd);

                }
                else if (movementStatus.Equals(MovementStatus.MovingToBeforeEnd))
                {
                    if (logic.GetDistanceToDestination() < 0.5f)
                    {
                        logic.SetMovementStatus(MovementStatus.IdleAtBeforeEnd);
                    }
                }
                else if (movementStatus.Equals(MovementStatus.IdleAtBeforeEnd))
                {
                    // TODO Rotation towards the end waypoint
                    logic.SetDestination(waypointEnd.position);
                    logic.SetMovementStatus(MovementStatus.MovingToEnd);
                }
                else if (movementStatus.Equals(MovementStatus.MovingToEnd))
                {
                    if (logic.GetDistanceToDestination() < 0.5f)
                    {
                        logic.SetMovementStatus(MovementStatus.IdleAtEnd);
                    }
                }
                else if (movementStatus.Equals(MovementStatus.IdleAtEnd))
                {
                    DestroyCustomer(customer);
                }
                
            }
        }
    }

    IEnumerator RotateCustomer(GameObject customer, RotationStatus targetRotationStatus)
    {
        if (customer == null) yield break;

        CustomerData data = customer.GetComponent<CustomerLogic>().data;
        Quaternion startRotation = data.getRotation();

        Transform target = customer.transform;
        Quaternion targetRotationQuaternion = Quaternion.identity;

        float rotationDegrees = 0f;
        float t = 0f;
        
        if(data.getRotationStatus().Equals(targetRotationStatus)) yield break;
        
        switch(targetRotationStatus)
        {
            case RotationStatus.RotatedTowardsPlayer:
                if (data.getRotationStatus().Equals(RotationStatus.RotatingTowardsPlayer)) yield break;
                data.setRotationStatus(RotationStatus.RotatingTowardsPlayer);

                rotationDegrees = Vector2.Angle(VectorTransform.ToVec2XZ(data.getDirection()), VectorTransform.ToVec2XZ(player.transform.position));
                Debug.Log("Rotation degrees: " + rotationDegrees);

                targetRotationQuaternion = Quaternion.Euler(target.eulerAngles + Vector3.up * rotationDegrees);
                break;
        }

        // Interpolation
        while (t < 1.0f)
        {
            t += Time.deltaTime / ROTATION_TIME;
            target.transform.rotation = Quaternion.Lerp(startRotation, targetRotationQuaternion, t);
            // * Wait one frame before looping again
            yield return null;
        }
        // * Stops the customer to turn further than the rotationDegrees
        customer.GetComponent<NavMeshAgent>().angularSpeed = 0f;
        data.setRotationStatus(targetRotationStatus);
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
            Debug.Log("Customer could not be destroyed");
            Debug.Log(e);
        }

    }

    public void loadCustomers(List<CustomerData> customerDataList)
    {
        foreach (CustomerData customerData in customerDataList)
        {
            GameObject customer = Instantiate(customerPrefab, customerData.getPos(), customerData.getRotation());
            customer.GetComponent<CustomerLogic>().Initialize(customerData);
            customers.Add(customer);
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