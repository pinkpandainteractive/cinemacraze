using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Settings")]
    [Range(1, 10)]
    public int MAX_CURRENT_CUSTOMERS_COUNT = 3;
    [Range(1, 10)]
    public float SPAWN_DELAY_SECONDS = 5f;
    [Range(1, 10)]
    public float ROTATION_TIME_SECONDS = 1f;


    public List<GameObject> customersList = new List<GameObject>();
    public int currentCustomersCount;
    public int totalCustomersCount;
    public float lastSpawnTime;
    public LiveCycleStatus status { get; set; }

    public GameObject customerPrefab;
    public Camera playerCamera;

    // * Waypoints
    public Transform waypointStart;
    public Transform waypointBar;
    public Transform waypointBeforeEnd;
    public Transform waypointEnd;

    public bool isGameRunning = false;

    public Queue<GameObject> toDestroyQueue = new Queue<GameObject>();


    void Start()
    {
        Reset();
    }
    void Update()
    {
        if (!isGameRunning) return;

        SpawnRoutine();
        CustomerRoutine();
        DestroyRoutine();
    }

    void SpawnRoutine()
    {
        if (currentCustomersCount >= MAX_CURRENT_CUSTOMERS_COUNT) return;
        if ((Time.time - lastSpawnTime) < SPAWN_DELAY_SECONDS) return;

        customersList.Add(SpawnCustomer());
    }

    GameObject SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, waypointStart.position, Quaternion.identity);

        long id = totalCustomersCount;
        string name = "Customer_" + id;

        customer.tag = "Customer";
        customer.name = name;
        customer.GetComponent<CustomerLogic>().Initialize(name, id, waypointStart.position, waypointStart.forward, waypointBar.position);

        lastSpawnTime = Time.time;
        totalCustomersCount++;
        currentCustomersCount++;

        Debug.Log("Customer spawned");

        return customer;
    }

    // TODO sachen auslagern is eigene Methoden
    void CustomerRoutine()
    {
        foreach (GameObject customer in customersList)
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
                        if (logic.GetDistanceToDestination() < 6.0f)
                            StartCoroutine(RotateCustomer(customer));
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
                    toDestroyQueue.Enqueue(customer);
                }

            }
        }
    }

    IEnumerator RotateCustomer(GameObject customerGameObject, bool onLoad = false)
    {
        CustomerData data = customerGameObject.GetComponent<CustomerLogic>().data;
        if (data.getRotationStatus().Equals(RotationStatus.RotatedTowardsPlayer)) yield break;
        if (data.getRotationStatus().Equals(RotationStatus.RotatingTowardsPlayer) && !onLoad) yield break;

        data.setRotationStatus(RotationStatus.RotatingTowardsPlayer);

        Quaternion startRotationQuaternion = data.getRotation();
        Transform targetTransform = customerGameObject.transform;

        float rotationDegrees = 0f;

        // TODO plan is to save the rotationPercent in the customerData so it can continue rotating after loading a save file
        float rotationPercent = data.getRotationPercent();

        rotationDegrees = Vector2.Angle(VectorTransformer.ToVec2xz(data.getDirection()), VectorTransformer.ToVec2xz(playerCamera.transform.position));

        Debug.Log("Rotation degrees: " + rotationDegrees);
        Quaternion rotationQuaternion = Quaternion.Euler(targetTransform.eulerAngles + Vector3.up * rotationDegrees);

        // Interpolation
        while (rotationPercent < 1.0f)
        {
            rotationPercent += Time.deltaTime / ROTATION_TIME_SECONDS;
            targetTransform.transform.rotation = Quaternion.Lerp(startRotationQuaternion, rotationQuaternion, rotationPercent);

            // * Wait one frame before looping again
            yield return null;
        }

        // * Stops the customer to turn further than the rotationDegrees
        customerGameObject.GetComponent<NavMeshAgent>().angularSpeed = 0f;
        data.setRotationStatus(RotationStatus.RotatedTowardsPlayer);
    }

    void DestroyRoutine()
    {
        while (toDestroyQueue.Count > 0)
        {
            GameObject customer = toDestroyQueue.Dequeue();
            customersList.Remove(customer);
            Destroy(customer);
            currentCustomersCount--;
            Debug.Log("Customer destroyed");
        }
    }

    public void LoadCustomers(List<CustomerData> customerDataList)
    {
        foreach (CustomerData customerData in customerDataList)
        {
            GameObject customer = Instantiate(customerPrefab, customerData.getPos(), customerData.getRotation());
            customer.GetComponent<CustomerLogic>().Initialize(customerData);
            StartCoroutine(RotateCustomer(customer, true));
            customersList.Add(customer);
        }
    }

    public void Reset()
    {
        foreach (GameObject customer in customersList)
        {
            if (customer == null) continue;
            Destroy(customer);
        }
        customersList.Clear();
        currentCustomersCount = 0;
        totalCustomersCount = 0;
        lastSpawnTime = -5f;

        status = LiveCycleStatus.Inactive;

    }

}

public enum LiveCycleStatus
{
    Inactive,
    Active,
    Paused
}