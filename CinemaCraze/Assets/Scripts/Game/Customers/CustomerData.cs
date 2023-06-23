using UnityEngine;
[System.Serializable]
public class CustomerData
{
    public MovementStatus movementStatus { get; set; }

    public string name { get; set; }
    public long id { get; set; }

    public Vector3 pos { get; set; }
    public Vector3 direction { get; set; }
    public Vector3 destination { get; set; }

    public CustomerOrder order { get; set; }

    public CustomerData(string name, long id, Vector3 pos, Vector3 direction, Vector3 destination)
    {
        movementStatus = MovementStatus.Undefined;
        this.name = name;
        this.id = id;
        this.pos = pos;
        this.direction = direction;
        this.destination = destination;
        this.order = new CustomerOrder();
    }

}