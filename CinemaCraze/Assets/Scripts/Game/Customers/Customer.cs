using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public CustomerManager customerManager;
    public Order order;

    NavMeshAgent navMeshAgent;
    OrderStatus orderStatus;
    MovementStatus movementStatus;
    GameObject customer;
    Vector3 pos;
    Vector3 bar;
    Vector3 end;
    string tag;
    string name;


    public void Init(GameObject customer)
    {
        this.customer = customer;
        this.customerManager = customerManager;

        this.navMeshAgent = customer.GetComponent<NavMeshAgent>();
        this.bar = customerManager.waypointBar.position;
        this.end = customerManager.waypointEnd.position;

        this.tag = customer.tag;
        this.name = customer.name;

        this.orderStatus = OrderStatus.None;

        navMeshAgent.SetDestination(customerManager.waypointBar.position);
        this.movementStatus = MovementStatus.Moving;
    }
    void Update()
    {
        if (!customerManager.status.Equals(LiveCycleStatus.Active)) return;
        if (customer == null) return;

        UpdateData();
        CheckPosition();
    }

    void UpdateData()
    {
        pos = customer.transform.position;
    }

    void CheckPosition()
    {
        NavMeshAgent navMeshAgent = customer.GetComponent<NavMeshAgent>();
        

        if (movementStatus.Equals(MovementStatus.Idle)) return;

        if (ArrivedAtBar()) RoutineBar();
        if (ArrivedAtEnd()) RoutineEnd();
    }

    bool ArrivedAtBar()
    {
        return Vector3.Distance(pos, bar) < 4f;
    }

    void RoutineBar()
    {
        movementStatus = MovementStatus.Idle;
        PlaceOrder();
        orderStatus = OrderStatus.Ordering;
    }

    void PlaceOrder()
    {
        float seed = Random.Range(0f, 1f);
        order.GenerateOrder(seed);
        Debug.Log("Order placed");
    }

    bool ArrivedAtEnd()
    {
        return Vector3.Distance(pos, end) < 0.5f;
    }

    void RoutineEnd()
    {
        Debug.Log("RoutineEnd");
    }
}


public enum OrderStatus
{
    None,
    Ordering,
    InProgress,
    Completed
}

public enum MovementStatus
{
    None,
    Idle,
    Moving
}