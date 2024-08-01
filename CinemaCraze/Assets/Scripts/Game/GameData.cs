using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int lives;
    public int score;
    public int invNachos;
    public int invPopcorn;
    public int invSoda;
    public List<CustomerData> customers;
    public int customerCount;
    public int totalCustomerCount;
    public float timeOfLastSpawn;
    public bool popcornUnlocked;
    public bool sodaUnlocked;
    public bool nachosUnlocked;

    
     public List<ProductData> products;
    public List<UpgradesData> upgrades;
    public GameData(Lives lives, Score score, Inventory inventory, CustomerManager customerManager, MachineManager machineManager,
    Product popcorn, Product nacho, Product soda, Upgrades popcornUpgrades, Upgrades nachoUpgrades, Upgrades sodaUpgrades
    )
    {
        this.lives = lives.lives;
        this.score = score.score;
        this.invNachos = inventory.nachos;
        this.invPopcorn = inventory.popcorn;
        this.invSoda = inventory.soda;

        this.customerCount = customerManager.currentCustomersCount;
        this.totalCustomerCount = customerManager.totalCustomersCount;
        this.timeOfLastSpawn = customerManager.lastSpawnTime;

        this.customers = new List<CustomerData>();
        foreach (GameObject customer in customerManager.customersList)
        {
            if (customer == null) continue;
            this.customers.Add(customer.GetComponent<CustomerLogic>().data);
        }
        
        
        this.products = new List<ProductData>
        {
            popcorn.GetProductData(),
            nacho.GetProductData(),
            soda.GetProductData()
        };
        this.upgrades = new List<UpgradesData>
        {
            popcornUpgrades.GetUpgradesData(),
            nachoUpgrades.GetUpgradesData(),
            sodaUpgrades.GetUpgradesData()
        };
        this.popcornUnlocked = machineManager.popcornMachineUnlocked;
        this.sodaUnlocked = machineManager.sodaMachineUnlocked;
        this.nachosUnlocked = machineManager.nachosMachineUnlocked;
    }

}