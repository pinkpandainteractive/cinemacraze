using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Customer : MonoBehaviour
{
    public CustomerManager customerManager;
    public TMP_Text orderText;


    public Order order;

    public Inventory inventory;
    public Score score;
    public Lives lives;

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

        UpdatePosition();
        CheckPosition();
    }

    void UpdatePosition()
    {
        pos = customer.transform.position;
    }

    void CheckPosition()
    {
        if (movementStatus.Equals(MovementStatus.Moving))
        {
            if (ArrivedAtBar()) RoutineBar();
            if (ArrivedAtEnd()) RoutineEnd();
        }

    }

    bool ArrivedAtBar()
    {
        return Vector3.Distance(pos, bar) < 4f;
    }

    void RoutineBar()
    {
        movementStatus = MovementStatus.Idle;
        orderStatus = OrderStatus.Ordering;
        PlaceOrder();
        UpdateOrderText();
    }

    void PlaceOrder()
    {
        orderStatus = OrderStatus.InProgress;
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

    void UpdateOrderText()
    {
        string textPopcorn = "";
        string textNachos = "";
        string textSoda = "";

        if (order.nPopcorn > 0) textPopcorn = "Popcorn:\t" + order.nPopcorn + "\n";
        if (order.nNachos > 0) textNachos = "Nachos:\t" + order.nNachos + "\n";
        if (order.nSoda > 0) textSoda = "Soda:\t" + order.nSoda + "\n";

        orderText.text = textPopcorn + textNachos + textSoda;
    }

    public void HandInOrder()
    {
        if (CheckMatch(order, inventory))
        {
            Debug.Log("Order correct");
        }
        else
        {
            Debug.Log("Order incorrect");
        }

        orderStatus = OrderStatus.Completed;
    }

    bool CheckMatch(Order order, Inventory inventory)
    {
        Debug.Log(order.nNachos + " " + order.nPopcorn + " " + order.nSoda);
        return order.nPopcorn == inventory.nPopcorn && order.nNachos == inventory.nNachos && order.nSoda == inventory.nSoda;
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