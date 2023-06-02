using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour {
    
    public int nNachos = 0;
    public int nPopcorn = 0;
    public int nSoda = 0;

    public TMP_Text inventoryText;

    public void AddNachos(int amount) {
        nNachos += amount;
        UpdateInventoryText();
        Debug.Log("Nachos: " + nNachos);
    }
    public void AddPopcorn(int amount) {
        nPopcorn += amount;
        UpdateInventoryText();
        Debug.Log("Popcorn: " + nPopcorn);
    }
    public void AddSoda(int amount) {
        nSoda += amount;
        UpdateInventoryText();
        Debug.Log("Soda: " + nSoda);
    }

    public void RemoveNachos(int amount) {
        nNachos -= amount;
        UpdateInventoryText();
        Debug.Log("Nachos: " + nNachos);
    }
    public void RemovePopcorn(int amount) {
        nPopcorn -= amount;
        UpdateInventoryText();
        Debug.Log("Popcorn: " + nPopcorn);
    }
    public void RemoveSoda(int amount) {
        nSoda -= amount;
        UpdateInventoryText();
        Debug.Log("Soda: " + nSoda);
    }

    public void Clear() {
        nNachos = 0;
        nPopcorn = 0;
        nSoda = 0;
        UpdateInventoryText();
    }

    void UpdateInventoryText () {
        string nachosText = "";
        string popcornText = "";
        string sodaText = "";

        if (nNachos > 0) nachosText = "Nachos:\t" + nNachos + "\n";
        if (nPopcorn > 0) popcornText = "Popcorn:\t" + nPopcorn + "\n";
        if (nSoda > 0) sodaText = "Soda:\t" + nSoda + "\n";

        inventoryText.text = nachosText + popcornText + sodaText;
    }
}