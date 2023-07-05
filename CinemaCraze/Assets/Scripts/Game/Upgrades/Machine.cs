using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;

public class Machine : MonoBehaviour
{
    public MachineStatus machineStatus;
    public BuyScreenStatus buyScreenStatus;

    public MachineManager machineManager;

    public GameObject product;
   // public int price = 100;
    public Score score;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI scoreText;
    public Button button;
    
    void Start()
    {
        machineManager.machines.Add(gameObject);
        machineStatus = MachineStatus.None;
        buyScreenStatus = BuyScreenStatus.InActive;
        priceText.text = $"Kaufe die Maschine für:";
        scoreText.text = $"{machineManager.price}";
        GetComponent<Renderer>().material = machineManager.mtl_locked;
        
        product.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        button.onClick.AddListener(BuyMachine);
    }
    void Update()
    {
        HandleButtonColor();
        UpdatePrice();
    }
    public void UpdatePrice()
    {
        scoreText.text = $"{machineManager.price}";
        
    }
    void HandleButtonColor()
    {
        if (score.GetScore() >= machineManager.price)
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            // * Not enough money to buy the machine
            button.GetComponent<Image>().color = Color.red;
        }
    }
    public void BuyMachine()
    {
        machineManager.HandleBuyMachineProcess(gameObject);
    }



  
    private void OnMouseOver()
    {
        if(machineStatus == MachineStatus.Owned) return;
        GetComponent<Renderer>().material = machineManager.mtl_hover;
    }
    private void OnMouseExit()
    {
        if (machineStatus == MachineStatus.Owned) return;
        GetComponent<Renderer>().material = machineManager.mtl_locked;
    }
    public enum MachineStatus
    {
        None,
        Owned
    }
    
    public enum BuyScreenStatus
    {
        InActive,
        Active
    }
}
