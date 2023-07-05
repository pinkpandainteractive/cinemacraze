using System;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{

    const float DISTANCE_BETWEEN_CUSTOMERS = 4.5f;

    public CustomerData data;
    public TMP_Text orderText;
    public Inventory inventory;
    public GameObject orderBubble;
    public Score score;
    public Lives lives;
    public NavMeshAgent navMeshAgent;

    public MovementStatus movementStatus;
    public float distanceToDestination;


    public void Initialize(string name, long id, Vector3 pos, Vector3 direction, Vector3 destination)
    {
        Debug.Log("Initialize Customer");
        this.data = new CustomerData(name, id, pos, direction, destination);
        navMeshAgent.SetDestination(destination);
        SetMovementStatus(MovementStatus.MovingToBar);
    }

    // * Use this for initialization after loading from save
    public void Initialize(CustomerData data)
    {
        Debug.Log("Initialize Customer");
        this.data = data;
        navMeshAgent.SetDestination(data.getDestination());
        SetMovementStatus(data.getMovementStatus());
    }

    public void GenerateOrder()
    {
        data.getOrder().GenerateOrder();
        UpdateOrderText();
    }

    public void HandInOrder()
    {
        Debug.Log("HandInOrder");

        if (!data.getOrder().status.Equals(OrderStatus.Ordering)) return;

        if (data.getOrder().popcorn > 0 && inventory.popcorn > 0)
        {
            while (data.getOrder().popcorn > 0 && inventory.popcorn > 0)
            {
                data.getOrder().popcorn--;
                inventory.RemovePopcorn(1);
            }
        }
        if (data.getOrder().soda > 0 && inventory.soda > 0)
        {
            while (data.getOrder().soda > 0 && inventory.soda > 0)
            {
                data.getOrder().soda--;
                inventory.RemoveSoda(1);
            }
        }
        if (data.getOrder().nachos > 0 && inventory.nachos > 0)
        {
            while (data.getOrder().nachos > 0 && inventory.nachos > 0)
            {
                data.getOrder().nachos--;
                inventory.RemoveNachos(1);
            }
        }

        if (data.getOrder().popcorn == 0 && data.getOrder().soda == 0 && data.getOrder().nachos == 0)
        {
            Debug.Log("Order completed");
            data.getOrder().status = OrderStatus.Completed;
            navMeshAgent.isStopped = false;
        }

        UpdateOrderText();
    }

    public float GetDistanceToDestination()
    {
        return Vector3.Distance(data.getPos(), data.getDestination());
    }

    public void UpdatePosition(Transform transform)
    {
        data.setPos(transform.position);
        data.setDirection(transform.forward);
        data.setRotation(transform.rotation);
    }

    public void KeepDistanceToOtherCustomers()
    {
        if (!data.getOrder().status.Equals(OrderStatus.Ordering)) return;

        // * Required for OverlapCapsule
        var direction = new Vector3 { [GetComponent<CapsuleCollider>().direction] = 1 };
        var offset = GetComponent<CapsuleCollider>().height / 2 - GetComponent<CapsuleCollider>().radius;
        var localPoint0 = GetComponent<CapsuleCollider>().center - direction * offset;
        var localPoint1 = GetComponent<CapsuleCollider>().center + direction * offset;
        var point0 = transform.TransformPoint(localPoint0);
        var point1 = transform.TransformPoint(localPoint1);

        // * Changed OverlapBox collider to OverlapCapsule collider
        Collider[] colliders = Physics.OverlapCapsule(point0, point1, GetComponent<CapsuleCollider>().radius, LayerMask.GetMask("Customer"));

        foreach (Collider collider in colliders)
        {

            // * check if collider is a customer
            if (collider.gameObject.tag != "Customer") continue;

            CustomerLogic otherCustomerLogic = collider.gameObject.GetComponent<CustomerLogic>();
            if (otherCustomerLogic == null) continue;

            // * check if collider is not this customer
            if (otherCustomerLogic.data.getId() == data.getId()) continue;
            Vector3 otherPos = otherCustomerLogic.data.getPos();
            Vector3 directionToOtherCustomer = (otherPos - data.getPos());
            float distanceToOtherCustomer = directionToOtherCustomer.magnitude;

            // * Check if other customer is too close in positive z direction
            if (distanceToOtherCustomer <= DISTANCE_BETWEEN_CUSTOMERS && directionToOtherCustomer.normalized.z > 0.8)
            {
                if (!navMeshAgent.isStopped)
                    navMeshAgent.isStopped = true;
                
                SetMovementStatus(MovementStatus.IdleAtBar);
                return;
            }

        }

        // * Activate NavMeshAgent if no other customer is too close in front
        navMeshAgent.isStopped = false;
        SetMovementStatus(MovementStatus.MovingToBar);

    }

    public void UpdateOrderText()
    {
        orderText.text = data.getOrder().ToString();
        if (orderText.text.Equals(""))
        {
            orderBubble.SetActive(false);
        }
        else
        {
            orderBubble.SetActive(true);
        }

        // ! this is just here for debugging in inspector
        distanceToDestination = GetDistanceToDestination();
    }
    public void SetDestination(Vector3 destination)
    {
        Debug.Log(data.getName() + "SetDestination to: " + destination);
        navMeshAgent.SetDestination(destination);
        data.setDestination(destination);
    }

    public void SetMovementStatus(MovementStatus movementStatus)
    {
        if (this.movementStatus == movementStatus) return;
        Debug.Log(data.getName() + "SetMovementStatus to:" + movementStatus);
        data.setMovementStatus(movementStatus);
        this.movementStatus = movementStatus;
    }

    public Vector3 getPosition()
    {
        return data.getPos();
    }

    public Vector3 getDirection()
    {
        return data.getDirection();
    }

    public Vector3 GetDestination()
    {
        return data.getDestination();
    }

    public OrderStatus GetOrderStatus()
    {
        return data.getOrder().status;
    }

    public MovementStatus GetMovementStatus()
    {
        return data.getMovementStatus();
    }




}