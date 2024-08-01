using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[System.Serializable]
    public class ProductData
    {
    public ProductionStatus productionStatus;
    public CapacityStatus capacityStatus;
    public RefillStatus refillStatus;

    public float productionTime;
    public int productionLevel;
    public float refillTime;
    public int refillLevel;
    public int maxCapacity;
    public int maxCapacityLevel;
    public int capacity;

     public enum ProductionStatus
    {
        None,
        Waiting,
        Done
    }
    public enum RefillStatus
    {
        None,
        Filling,
        Done
    }
    public enum CapacityStatus
    {
        Empty,
        Available,
        Full
    }
    }
