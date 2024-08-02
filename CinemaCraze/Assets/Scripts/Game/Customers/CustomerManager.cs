using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Settings")]
    [Range(1, 10)]
    public int MAX_CURRENT_CUSTOMERS_COUNT = 3;
    [Range(1, 10)]
    public float SPAWN_DELAY_SECONDS = 5f;
    [Range(1, 10)]
    public float ROTATION_TIME_SECONDS = 1f;

    public Material mtl_pink;
    public Material mtl_blue;
    public Material mtl_red;
    public Material mtl_black;
    public Material mtl_legendary;
    public Material mtl_lila;
    public Material mtl_cyan;
    public Material mtl_orange;
    public List<GameObject> customersList = new List<GameObject>();
    public int currentCustomersCount;
    public int totalCustomersCount;
    public float lastSpawnTime;
    public LiveCycleStatus status { get; set; }

    public GameObject customerPrefab;
    public Camera playerCamera;

    public MachineManager machineManager;

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
        if (!machineManager.popcornMachineUnlocked && !machineManager.nachosMachineUnlocked && !machineManager.sodaMachineUnlocked) return;
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
        RandomCustomerColor(customer);
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
    void RandomCustomerColor(GameObject customer)
    {
        List<Material> materials = customer.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials.ToList();

        var brows = materials[0];
        var body = materials[1];
        var eye = materials[2]; // * wrong eyes

        // * right eyes
        List<Material> materialsEyes1 = customer.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().materials.ToList();
        var eye1 = materialsEyes1[0];
        List<Material> materialsEyes2 = customer.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().materials.ToList();
        var eye2 = materialsEyes2[0];
        List<Material> materialsEyes3 = customer.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().materials.ToList();
        var eye3 = materialsEyes3[0];

        float random = Random.Range(0, 10);

        if (random > 2 && random <= 3)
        {
            eye1.color = mtl_red.color;
            eye2.color = mtl_red.color;
            eye3.color = mtl_red.color;
            brows.color = mtl_orange.color;
            body.color = mtl_blue.color;
        }
        //Orange Guy
        else if (random > 3 && random <= 4)
        {
            //eye1.color = mtl_cyan.color;
            //eye2.color = mtl_cyan.color;
            //eye3.color = mtl_cyan.color;
            brows.color = mtl_orange.color;
            body.color = mtl_orange.color;
        }
        // Cyan Red eyes Guy (stoned)
        else if (random > 4 && random <= 5)
        {
            eye1.color = mtl_red.color;
            eye2.color = mtl_red.color;
            eye3.color = mtl_red.color;
            brows.color = mtl_cyan.color;
            body.color = mtl_cyan.color;
        }
        // Red Blue Guy
        else if (random > 5 && random <= 6)
        {
            eye1.color = mtl_blue.color;
            eye2.color = mtl_blue.color;
            eye3.color = mtl_blue.color;
            brows.color = mtl_blue.color;
            body.color = mtl_red.color;
        }// Lila Guy
        else if (random > 6 && random <= 7)
        {
            eye1.color = mtl_cyan.color;
            eye2.color = mtl_cyan.color;
            eye3.color = mtl_cyan.color;
            brows.color = mtl_blue.color;
            body.color = mtl_lila.color;
        }// Again orange guy but with no eyes
        else if (random > 8 && random <= 9)
        {
            eye1.color = mtl_orange.color;
            eye2.color = mtl_orange.color;
            eye3.color = mtl_orange.color;
            brows.color = mtl_cyan.color;
            body.color = mtl_orange.color;
        }// Epic
        else if (random > 9 && random <= 9.5)
        {
            eye1.color = mtl_legendary.color;
            eye2.color = mtl_legendary.color;
            eye3.color = mtl_legendary.color;
            brows.color = mtl_legendary.color;
            body.color = mtl_black.color;
        }
        // Albino
        else if (random > 9.5 && random <= 9.9)
        {
            eye.color = Color.white;
            brows.color = Color.white;
            body.color = Color.white;
        }// Legendary 
        else if (random >= 9.9)
        {
            eye1.color = mtl_legendary.color;
            eye2.color = mtl_legendary.color;
            eye3.color = mtl_legendary.color;
            brows.color = mtl_legendary.color;
            body.color = mtl_legendary.color;
        }
    }
    // TODO sachen auslagern is eigene Methoden
    void CustomerRoutine()
    {
        foreach (GameObject customer in customersList)
        {
            CustomerLogic logic = customer.GetComponent<CustomerLogic>();
            logic.UpdatePosition(customer.transform);
            logic.UpdateOrderText();

            OrderStatus orderStatus = logic.GetOrderStatus();
            MovementStatus movementStatus = logic.GetMovementStatus();

            if (orderStatus.Equals(OrderStatus.Undefined))
            {
                if (logic.GetDistanceToDestination() < 6.0f)
                {
                    logic.GenerateOrder(machineManager.popcornMachineUnlocked, machineManager.sodaMachineUnlocked, machineManager.nachosMachineUnlocked);
                    StartCoroutine(RotateCustomer(customer));
                }
            }
            else if (orderStatus.Equals(OrderStatus.Ordering))
            {
                if (movementStatus.Equals(MovementStatus.MovingToBar))
                {
                    if (logic.GetDistanceToDestination() < 0.5f)
                    {
                        logic.SetMovementStatus(MovementStatus.IdleAtBar);
                    }
                    else
                    {
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
                if (movementStatus.Equals(MovementStatus.IdleAtBar) || movementStatus.Equals(MovementStatus.MovingToBar))
                {
                    if (orderStatus.Equals(OrderStatus.Failed))
                    {
                        logic.lives.LoseLife();
                    }
                    else
                    {
                        logic.score.AddScore(logic.data.getOrder().value);
                    }

                    customer.GetComponent<NavMeshAgent>().angularSpeed = 120f;

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

        rotationDegrees = Vector2.Angle(
            new Vector2(data.getDirection().x, data.getDirection().z),
            new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.z));

        Quaternion rotationQuaternion = Quaternion.Euler(targetTransform.eulerAngles + Vector3.up * rotationDegrees);

        // * Interpolation
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