using UnityEngine;

public class Customer : MonoBehaviour {
    public OrderStatus orderStatus { get; set;}
    public MovementStatus movementStatus { get; set;}
    public GameObject customer { get; set;}
}


public enum OrderStatus {
    None,
    InProgress,
    Completed
}

public enum MovementStatus {
    Idle,
    Moving
}