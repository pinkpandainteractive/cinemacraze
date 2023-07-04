using UnityEngine;
[System.Serializable]
public class CustomerData
{
    MovementStatus movementStatus;
    RotationStatus rotationStatus;
    float rotationPercent;

    string name;
    long id;

    float[] pos = new float[3];
    float[] direction = new float[3];
    float[] rotation = new float[4];
    float[] destination = new float[3];

    public CustomerOrder order;

    public CustomerData(string name, long id, Vector3 pos, Vector3 direction, Vector3 destination)
    {
        movementStatus = MovementStatus.Undefined;
        rotationStatus = RotationStatus.Undefined;
        rotationPercent = 0f;

        this.name = name;
        this.id = id;

        this.pos[0] = pos.x;
        this.pos[1] = pos.y;
        this.pos[2] = pos.z;

        this.direction[0] = direction.x;
        this.direction[1] = direction.y;
        this.direction[2] = direction.z;

        this.destination[0] = destination.x;
        this.destination[1] = destination.y;
        this.destination[2] = destination.z;

        order = new CustomerOrder();
    }

    public void setPos(Vector3 pos)
    {
        this.pos[0] = pos.x;
        this.pos[1] = pos.y;
        this.pos[2] = pos.z;
    }

    public void setDirection(Vector3 direction)
    {
        this.direction[0] = direction.x;
        this.direction[1] = direction.y;
        this.direction[2] = direction.z;
    }

    public void setRotation(Quaternion rotation)
    {
        this.rotation[0] = rotation.x;
        this.rotation[1] = rotation.y;
        this.rotation[2] = rotation.z;
        this.rotation[3] = rotation.w;
    }

    public void setDestination(Vector3 destination)
    {
        this.destination[0] = destination.x;
        this.destination[1] = destination.y;
        this.destination[2] = destination.z;
    }

    public void setMovementStatus(MovementStatus movementStatus)
    {
        this.movementStatus = movementStatus;
    }

    public void setRotationStatus(RotationStatus rotationStatus)
    {
        this.rotationStatus = rotationStatus;
    }

    public void setRotationPercent(float rotationPercent)
    {
        this.rotationPercent = rotationPercent;
    }

    public void setOrder(CustomerOrder order)
    {
        this.order = order;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public void setId(long id)
    {
        this.id = id;
    }

    public Vector3 getPos()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }

    public Vector3 getDirection()
    {
        return new Vector3(direction[0], direction[1], direction[2]);
    }

    public Quaternion getRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }

    public Vector3 getDestination()
    {
        return new Vector3(destination[0], destination[1], destination[2]);
    }

    public MovementStatus getMovementStatus()
    {
        return movementStatus;
    }

    public RotationStatus getRotationStatus()
    {
        return rotationStatus;
    }

    public float getRotationPercent()
    {
        return rotationPercent;
    }

    public string getName()
    {
        return name;
    }

    public long getId()
    {
        return id;
    }

    public CustomerOrder getOrder()
    {
        return order;
    }

    public override string ToString()
    {
        return "CustomerData: " + name + " " + id + " " + getPos() + " " + getDirection() + " " + getDestination();
    }

}