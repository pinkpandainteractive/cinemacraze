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
    //public List<GameObject> customers;
    public int customerCount;
    public int totalCustomerCount;
    public float timeOfLastSpawn;

    public GameData(Lives lives, Score score, Inventory inventory, CustomerManager customerManager)
    {
        this.lives = lives.lives;
        this.score = score.score;
        this.invNachos = inventory.nachos;
        this.invPopcorn = inventory.popcorn;
        this.invSoda = inventory.soda;

        //this.customers = customerManager.customers;
        this.customerCount = customerManager.customerCount;
        this.totalCustomerCount = customerManager.totalCustomerCount;
        this.timeOfLastSpawn = customerManager.timeOfLastSpawn;
    }

}