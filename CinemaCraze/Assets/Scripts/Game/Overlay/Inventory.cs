using UnityEngine;

public class Inventory : MonoBehaviour {
    
    private int nachos = 0;
    private int popcorn = 0;
    private int soda = 0;

    public void AddNachos(int amount) {
        nachos += amount;
        Debug.Log("Nachos: " + nachos);
    }
    public void AddPopcorn(int amount) {
        popcorn += amount;
        Debug.Log("Popcorn: " + popcorn);
    }
    public void AddSoda(int amount) {
        soda += amount;
        Debug.Log("Soda: " + soda);
    }

    public void RemoveNachos(int amount) {
        nachos -= amount;
        Debug.Log("Nachos: " + nachos);
    }
    public void RemovePopcorn(int amount) {
        popcorn -= amount;
        Debug.Log("Popcorn: " + popcorn);
    }
    public void RemoveSoda(int amount) {
        soda -= amount;
        Debug.Log("Soda: " + soda);
    }

    public int GetNachos() {
        return nachos;
    }
    public int GetPopcorn() {
        return popcorn;
    }
    public int GetSoda() {
        return soda;
    }

    public void SetNachos(int amount) {
        nachos = amount;
        Debug.Log("Nachos: " + nachos);
    }
    public void SetPopcorn(int amount) {
        popcorn = amount;
        Debug.Log("Popcorn: " + popcorn);
    }
    public void SetSoda(int amount) {
        soda = amount;
        Debug.Log("Soda: " + soda);
    }

    public void Clear() {
        nachos = 0;
        popcorn = 0;
        soda = 0;
    }
}