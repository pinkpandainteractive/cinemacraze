using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Machine;
using static RandomUnlock;

public class MachineManager : MonoBehaviour
{
    public AudioHandler audioHandler;
    public Score score;
    public Material matl_colorPalette;
    public Material mtl_locked;
    public Material mtl_hover;

    public const int NUMBER_OF_MACHINES = 3;
    public bool popcornMachineUnlocked;
    public bool nachosMachineUnlocked;
    public bool sodaMachineUnlocked;

    public GameObject popcornMachine;
    public GameObject nachosMachine;
    public GameObject sodaMachine;

    public List<GameObject> machines;
    public int price;

    public float timeFirstBuy;
    public bool isFirstBuy = false;
    public bool isSecondBuy = false;
    public float timeSecondBuy;
   
    public void HandleBuyMachineProcess(GameObject gameObject)
    {
        Debug.Log("HandleBuyMachineProcess");
        if (gameObject == null) return;
        if (gameObject.GetComponent<Machine>().machineStatus.Equals(MachineStatus.Owned)) return;


        UpdatePrice();
        if (score.GetScore() < price)
        {
            Debug.Log("Not enough money to buy the machine");
            return;
        }
        audioHandler.PlayBuyUpgrades();
        gameObject.GetComponent<Machine>().machineStatus = MachineStatus.Owned;

        gameObject.GetComponent<Machine>().product.SetActive(true);

        gameObject.GetComponent<Renderer>().material = matl_colorPalette;
        score.SubtractScore(price);

        if (gameObject.name.Equals("Popcornmachine"))
        {
            popcornMachineUnlocked = true;
        }
        else if (gameObject.name.Equals("Nachomachine"))
        {
            nachosMachineUnlocked = true;
        }
        else if (gameObject.name.Equals("Sodamachine"))
        {
            sodaMachineUnlocked = true;
        }
        if(!isSecondBuy && isFirstBuy){
            isSecondBuy = true;
            timeSecondBuy = Time.time;
        }
        if(!isFirstBuy)
        {
            isFirstBuy = true;
            timeFirstBuy = Time.time;
        }
        
        HandleBuyScreen(gameObject);
    }

    public void HandleBuyScreen(GameObject gameObject)
    {
        if (gameObject.GetComponent<Machine>().machineStatus.Equals(MachineStatus.Owned))
        {

            CloseBuyScreen(gameObject);
            return;
        }
        if (gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.InActive))
        {
            audioHandler.PlayClick();
            ShowBuyScreen(gameObject);
        }
        else
        {
            audioHandler.PlayClick();
            CloseBuyScreen(gameObject);
        }
    }
    private void UpdatePrice()
    {
        int count = 0;
        foreach (GameObject machine in machines)
        {
            if (machine.GetComponent<Machine>().machineStatus.Equals(MachineStatus.Owned))
            {
                count++;
            }
        }

        //int price = gameObject.GetComponent<Machine>().price;

        if (count == 0)
        {
            price = 0;
        }
        else if (count == 1)
        {
            price = 100;
        }
        else if (count >=2)
        {
            price = 250;
        }
    }
    public void ShowBuyScreen(GameObject gameObject)
    {
        if (gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.Active)) return;
        // * Accessing the Canvas with GetChild(0)
        UpdatePrice();
        
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<Machine>().buyScreenStatus = BuyScreenStatus.Active;
    }
    public void CloseBuyScreen(GameObject gameObject)
    {
        if (gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.InActive)) return;
        UpdatePrice();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Machine>().buyScreenStatus = BuyScreenStatus.InActive;

    }

    public void Reset()
    {
        popcornMachineUnlocked = false;
        nachosMachineUnlocked = false;
        sodaMachineUnlocked = false;

        popcornMachine.GetComponent<Machine>().machineStatus = MachineStatus.None;
        nachosMachine.GetComponent<Machine>().machineStatus = MachineStatus.None;
        sodaMachine.GetComponent<Machine>().machineStatus = MachineStatus.None;

        popcornMachine.GetComponent<Machine>().product.SetActive(false);
        nachosMachine.GetComponent<Machine>().product.SetActive(false);
        sodaMachine.GetComponent<Machine>().product.SetActive(false);

        popcornMachine.GetComponent<Renderer>().material = mtl_locked;
        nachosMachine.GetComponent<Renderer>().material = mtl_locked;
        sodaMachine.GetComponent<Renderer>().material = mtl_locked;
    }

    public void LoadMachines()
    {
        if(popcornMachineUnlocked)
        {
            popcornMachine.GetComponent<Machine>().machineStatus = MachineStatus.Owned;
            popcornMachine.GetComponent<Machine>().product.SetActive(true);
            popcornMachine.GetComponent<Renderer>().material = matl_colorPalette;
        }

        if (nachosMachineUnlocked)
        {
            nachosMachine.GetComponent<Machine>().machineStatus = MachineStatus.Owned;
            nachosMachine.GetComponent<Machine>().product.SetActive(true);
            nachosMachine.GetComponent<Renderer>().material = matl_colorPalette;
        }

        if (sodaMachineUnlocked)
        {
            sodaMachine.GetComponent<Machine>().machineStatus = MachineStatus.Owned;
            sodaMachine.GetComponent<Machine>().product.SetActive(true);
            sodaMachine.GetComponent<Renderer>().material = matl_colorPalette;
        }
    }

}
