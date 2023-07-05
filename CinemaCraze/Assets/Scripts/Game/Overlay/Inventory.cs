using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour {
    
    [Header("Inventory Settings")]
    [Tooltip("The maximum amount of items that can be stored in the inventory")]
    [Range(1, 100)]
    public int maxItems = 10;

    public int nachos = 0;
    public int popcorn = 0;
    public int soda = 0;

    public TMP_Text inventoryText;

    public void AddNachos(int amount) {
        nachos += amount;
        UpdateInventoryText();
        Debug.Log("Nachos: " + nachos);
    }
    public void AddPopcorn(int amount) {
        popcorn += amount;
        UpdateInventoryText();
        Debug.Log("Popcorn: " + popcorn);
    }
    public void AddSoda(int amount) {
        soda += amount;
        UpdateInventoryText();
        Debug.Log("Soda: " + soda);
    }

    public void RemoveNachos(int amount) {
        nachos -= amount;
        UpdateInventoryText();
        Debug.Log("Nachos: " + nachos);
    }
    public void RemovePopcorn(int amount) {
        popcorn -= amount;
        UpdateInventoryText();
        Debug.Log("Popcorn: " + popcorn);
    }
    public void RemoveSoda(int amount) {
        soda -= amount;
        UpdateInventoryText();
        Debug.Log("Soda: " + soda);
    }

    public void Clear() {
        Debug.Log("Clearing inventory");
        nachos = 0;
        popcorn = 0;
        soda = 0;
        UpdateInventoryText();
    }

    public void SetAll(int nachos, int popcorn, int soda) {
        this.nachos = nachos;
        this.popcorn = popcorn;
        this.soda = soda;
        UpdateInventoryText();
    }

    void UpdateInventoryText () {
        string nachosText = "";
        string popcornText = "";
        string sodaText = "";

        if (nachos > 0) nachosText = "Nachos:\t" + nachos + "\n";
        if (popcorn > 0) popcornText = "Popcorn:\t" + popcorn + "\n";
        if (soda > 0) sodaText = "Soda:\t\t" + soda + "\n";
        
        inventoryText.text = nachosText + popcornText + sodaText;
    }

    public bool HasSlotAvailable() {
        return (nachos + popcorn + soda) < maxItems;
    }
}