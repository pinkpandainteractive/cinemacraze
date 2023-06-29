using System;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System.Collections;

public class CustomerLogic : MonoBehaviour {

    const float DISTANCE_BETWEEN_CUSTOMERS = 4.5f;
    
    public CustomerData data;
    public TMP_Text orderText;
    public Inventory inventory;
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
        this.movementStatus = this.data.movementStatus = MovementStatus.MovingToBar;
    }

    public void GenerateOrder()
    {
        data.order.GenerateOrder();
        UpdateOrderText();
    }

    public void HandInOrder()
    {
        Debug.Log("HandInOrder");

        if (!data.order.status.Equals(OrderStatus.Ordering)) return;

        if (data.order.popcorn > 0 && inventory.popcorn > 0)
        {
            while (data.order.popcorn > 0 && inventory.popcorn > 0)
            {
                data.order.popcorn--;
                inventory.RemovePopcorn(1);
            }
        }
        if (data.order.soda > 0 && inventory.soda > 0)
        {
            while (data.order.soda > 0 && inventory.soda > 0)
            {
                data.order.soda--;
                inventory.RemoveSoda(1);
            }
        }
        if (data.order.nachos > 0 && inventory.nachos > 0)
        {
            while (data.order.nachos > 0 && inventory.nachos > 0)
            {
                data.order.nachos--;
                inventory.RemoveNachos(1);
            }
        }

        if (data.order.popcorn == 0 && data.order.soda == 0 && data.order.nachos == 0)
        {
            Debug.Log("Order completed");
            data.order.status = OrderStatus.Completed;
            score.AddScore(100);
            navMeshAgent.isStopped = false;
        }

        UpdateOrderText();
    }

    public float GetDistanceToDestination()
    {
        return Vector3.Distance(data.pos, data.destination);
    }

    public void UpdatePosition(Transform transform)
    {
        data.pos = transform.position;
        data.direction = transform.forward;
        data.rotation = transform.rotation;
    }

    public void KeepDistanceToOtherCustomers()
    {
        if (!data.order.status.Equals(OrderStatus.Ordering)) return;

        // * Required for OverlapCapsule
        var direction = new Vector3 { [GetComponent<CapsuleCollider>().direction] = 1 };
        var offset = GetComponent<CapsuleCollider>().height / 2 - GetComponent<CapsuleCollider>().radius;
        var localPoint0 = GetComponent<CapsuleCollider>().center - direction * offset;
        var localPoint1 = GetComponent<CapsuleCollider>().center + direction * offset;
        var point0 = transform.TransformPoint(localPoint0);
        var point1 = transform.TransformPoint(localPoint1);

        // * Changed OverlapBox collider to OverlapCapsule collider
        Collider[] colliders = Physics.OverlapCapsule(point0, point1, GetComponent<CapsuleCollider>().radius, LayerMask.GetMask("Customer"));
    
        foreach(Collider collider in colliders)
        {

            GameObject otherCustomer = collider.gameObject;

            // * check if collider is a customer
            if (otherCustomer.tag != "Customer") continue;

            // * check if collider is not this customer
            if (!otherCustomer == gameObject) continue;

            Vector3 otherPos = otherCustomer.GetComponent<CustomerLogic>().data.pos;
            Vector3 directionToOtherCustomer = (otherPos - data.pos);
            float distanceToOtherCustomer = directionToOtherCustomer.magnitude;

            // * Check if other customer is too close in positive z direction
            if (distanceToOtherCustomer <= DISTANCE_BETWEEN_CUSTOMERS && directionToOtherCustomer.normalized.z > 0.8)
            {
                if (!navMeshAgent.isStopped)
                {
                    navMeshAgent.isStopped = true;
                }
                SetMovementStatus(MovementStatus.IdleAtBar);
                return;
            }
        }

        // * Activate NavMeshAgent if no other customer is too close infront
        navMeshAgent.isStopped = false;
        SetMovementStatus(MovementStatus.MovingToBar);
    }

    public void UpdateOrderText()
    {
        orderText.text = data.order.ToString();
        distanceToDestination = GetDistanceToDestination();
    }
    public void SetDestination(Vector3 destination)
    {
        Debug.Log(data.name + "SetDestination to: " + destination);
        navMeshAgent.SetDestination(destination);
        data.destination = destination;
    }

    public void SetMovementStatus(MovementStatus movementStatus)
    {
        if (this.movementStatus == movementStatus) return;
        Debug.Log(data.name + "SetMovementStatus to:" + movementStatus);
        data.movementStatus = movementStatus;
        this.movementStatus = movementStatus;
    }

    public Vector3 getPosition()
    {
        return data.pos;
    }

    public Vector3 getDirection()
    {
        return data.direction;
    }

    public Vector3 GetDestination()
    {
        return data.destination;
    }

    public OrderStatus GetOrderStatus()
    {
        return data.order.status;
    }

    public MovementStatus GetMovementStatus()
    {
        return data.movementStatus;
    }




}