using UnityEngine;

public class Nachos : MonoBehaviour {
    
    public Inventory inventory;
    private void OnMouseDown() {
        Debug.Log("Nachos clicked");
        inventory.AddNachos(1);
    }

}