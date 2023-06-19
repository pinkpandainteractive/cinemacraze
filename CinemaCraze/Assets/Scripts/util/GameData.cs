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
    public int nCustomers;
    public int nTotalCustomers;
    public float tLastSpawn;

    public GameData(Lives lives, Score score, Inventory inventory, CustomerManager customerManager)
    {
        this.lives = lives.lives;
        this.score = score.score;
        this.invNachos = inventory.nNachos;
        this.invPopcorn = inventory.nPopcorn;
        this.invSoda = inventory.nSoda;

        //this.customers = customerManager.customers;
        this.nCustomers = customerManager.nCustomers;
        this.nTotalCustomers = customerManager.nTotalCustomers;
        this.tLastSpawn = customerManager.tLastSpawn;
    }

}