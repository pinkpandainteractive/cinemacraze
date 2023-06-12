using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Customer : MonoBehaviour
{
    public float CustomerDistance = 4.5f;
    public float BAR_RANGE = 5f;


    public CustomerManager customerManager;
    public TMP_Text orderText;

    public Order order;
    private Order.OrderInstance orderInstance;
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
        orderInstance = new Order.OrderInstance();

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
        return Vector3.Distance(pos, bar) < BAR_RANGE;
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
        orderInstance.GenerateOrder(seed);
        Debug.Log("Order placed");
    }


    void StayInLineWithOtherCustomers()
    {
        if (Vector3.Distance(pos, bar) < 0.4f) return;

        Collider[] colliders = Physics.OverlapBox(this.pos, this.customer.transform.localScale / 1.0f, Quaternion.identity, LayerMask.GetMask("Customer"));

        //Debug.Log("Customer " + this.customer.name + " is checking for other customers");
        //Debug.Log("Customer " + this.customer.name + " found " + colliders.Length + " colliders");

        foreach (Collider collider in colliders)
        {
            // * check if collider is a customer
            if (collider.gameObject.tag != this.customer.tag) continue;

            // * making sure that it is not its own collider
            GameObject otherCustomer = collider.gameObject;
            if (otherCustomer.name.Equals(this.customer.name)) continue;

            Vector3 otherPos = otherCustomer.transform.position;
            Vector3 directionToOtherCustomer = (otherPos - this.pos);
            float distanceToOtherCustomer = directionToOtherCustomer.magnitude;

            // * Check if other customer is too close in positive z direction
            //Debug.Log("Customer " + this.customer.name + " is checking for " + otherCustomer.name);
            //Debug.Log("Customer " + this.customer.name + " found " + distanceToOtherCustomer + " distance to " + otherCustomer.name);
            //Debug.Log("Customer " + this.customer.name + " found " + directionToOtherCustomer + " direction to " + otherCustomer.name);
            //Debug.Log("Customer " + this.customer.name + " found " + directionToOtherCustomer.normalized.z + " direction to " + otherCustomer.name);

            if (distanceToOtherCustomer < CustomerDistance && directionToOtherCustomer.normalized.z > 0.8)
            {
                if (!this.navMeshAgent.isStopped)
                {
                Debug.Log("Customer " + this.customer.name + " is waiting for " + otherCustomer.name + " to move");
                this.navMeshAgent.isStopped = true;
                //Debug.Log("navMeshAgent.isStopped = " + this.navMeshAgent.isStopped);
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

        if (orderInstance.nPopcorn > 0) textPopcorn = "Popcorn:\t" + orderInstance.nPopcorn + "\n";
        if (orderInstance.nNachos > 0) textNachos = "Nachos:\t" + orderInstance.nNachos + "\n";
        if (orderInstance.nSoda > 0) textSoda = "Soda:\t" + orderInstance.nSoda + "\n";

        orderText.text = textPopcorn + textNachos + textSoda;
    }

    public void HandInOrder()
    {

        if (inventory.nPopcorn > 0 && orderInstance.nPopcorn > 0)
        {
            while (inventory.nPopcorn > 0 && orderInstance.nPopcorn > 0)
            {
                inventory.RemovePopcorn(1);
                orderInstance.nPopcorn--;
            }
        }

        if (inventory.nNachos > 0 && orderInstance.nNachos > 0)
        {
            while (inventory.nNachos > 0 && orderInstance.nNachos > 0)
            {
                inventory.RemoveNachos(1);
                orderInstance.nNachos--;
            }
        }

        if (inventory.nSoda > 0 && orderInstance.nSoda > 0)
        {
            while (inventory.nSoda > 0 && orderInstance.nSoda > 0)
            {
                inventory.RemoveSoda(1);
                orderInstance.nSoda--;
            }
        }

        UpdateOrderText();

        if (orderInstance.nPopcorn == 0 && orderInstance.nNachos == 0 && orderInstance.nSoda == 0)
        {
            orderStatus = OrderStatus.Completed;
            Debug.Log("Order completed");
            score.AddScore(100);

            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(customerManager.waypointEnd.position);
            movementStatus = MovementStatus.Moving;
        }
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