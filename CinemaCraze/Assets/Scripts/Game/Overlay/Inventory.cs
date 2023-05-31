using UnityEngine;

public class Inventory : MonoBehaviour {
    
    public int nNachos = 0;
    public int nPopcorn = 0;
    public int nSoda = 0;

    public void AddNachos(int amount) {
        nNachos += amount;
        Debug.Log("Nachos: " + nNachos);
    }
    public void AddPopcorn(int amount) {
        nPopcorn += amount;
        Debug.Log("Popcorn: " + nPopcorn);
    }
    public void AddSoda(int amount) {
        nSoda += amount;
        Debug.Log("Soda: " + nSoda);
    }

    public void RemoveNachos(int amount) {
        nNachos -= amount;
        Debug.Log("Nachos: " + nNachos);
    }
    public void RemovePopcorn(int amount) {
        nPopcorn -= amount;
        Debug.Log("Popcorn: " + nPopcorn);
    }
    public void RemoveSoda(int amount) {
        nSoda -= amount;
        Debug.Log("Soda: " + nSoda);
    }

    public void Clear() {
        nNachos = 0;
        nPopcorn = 0;
        nSoda = 0;
    }
}