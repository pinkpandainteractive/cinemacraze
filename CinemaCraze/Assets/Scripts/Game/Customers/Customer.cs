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

    OrderStatus orderStatus;
    public MovementStatus movementStatus;

    GameObject customer;
    public NavMeshAgent navMeshAgent;

    public Vector3 pos;
    public Vector3 forward;

    Vector3 bar;
    Vector3 end;


    public void Init(GameObject customer)
    {
        this.customer = customer;

        bar = customerManager.waypointBar.position;
        end = customerManager.waypointEnd.position;

        orderStatus = OrderStatus.None;

        this.navMeshAgent.SetDestination(customerManager.waypointBar.position);
        movementStatus = MovementStatus.Moving;
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
        forward = customer.transform.forward;
    }

    void CheckPosition()
    {
        if (movementStatus.Equals(MovementStatus.Moving) && !orderStatus.Equals(OrderStatus.Completed))
        {
            if (ArrivedAtBar()) RoutineBar();
        }
        else if (movementStatus.Equals(MovementStatus.Idle))
        {
            if (ArrivedAtBar()) StayInLineWithOtherCustomers();
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
        movementStatus = MovementStatus.Idle;
        
        // destroy customer
        Destroy(customer);
        Destroy(this);
    }

    void StayInLineWithOtherCustomers()
    {
        if (Vector3.Distance(pos, bar) < 0.4f) return;

        Collider[] colliders = Physics.OverlapBox(this.pos, this.customer.transform.localScale / 1.0f, Quaternion.identity, LayerMask.GetMask("Customer"));

        // Debug.Log("Customer " + this.customer.name + " is checking for other customers");
        // Debug.Log("Customer " + this.customer.name + " found " + colliders.Length + " colliders");

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag != this.customer.tag) continue;

            GameObject otherCustomer = collider.gameObject;
            if (otherCustomer.name.Equals(this.customer.name)) continue;

            Vector3 otherPos = otherCustomer.transform.position;
            Vector3 directionToOtherCustomer = otherPos - this.pos;

            if (Vector3.Dot(directionToOtherCustomer, this.forward) > 0)
            {
                if (!this.navMeshAgent.isStopped)
                {
                    Debug.Log("Customer " + this.customer.name + " is waiting for " + otherCustomer.name + " to move");
                    this.navMeshAgent.isStopped = true;
                    Debug.Log("navMeshAgent.isStopped = " + this.navMeshAgent.isStopped);
                }

                return;
            }
        }

        this.navMeshAgent.isStopped = false;
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
        orderStatus = OrderStatus.Completed;

        if (CheckMatch(order, inventory))
        {
            Debug.Log("Order correct");
            inventory.Clear();
            
            // TODO score based on time and difficulty
            score.AddScore(100);
        }
        else
        {
            Debug.Log("Order incorrect");
            lives.LoseLife();
        }

        // just in case
        navMeshAgent.isStopped = false;

        navMeshAgent.SetDestination(customerManager.waypointEnd.position);
        movementStatus = MovementStatus.Moving;
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