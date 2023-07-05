using Unity.VisualScripting;
using UnityEngine;
using static Machine;

public class MachineManager : MonoBehaviour
{
    public AudioHandler audioHandler;
    public Score score;
    public Material matl_colorPalette;
    public Material mtl_locked;
    public Material mtl_hover;

    public bool popcornMachineUnlocked;
    public bool nachosMachineUnlocked;
    public bool sodaMachineUnlocked;

    public void HandleBuyMachineProcess(GameObject gameObject)
    {
        Debug.Log("HandleBuyMachineProcess");
        if (gameObject == null) return;
        if (gameObject.GetComponent<Machine>().machineStatus.Equals(MachineStatus.Owned)) return;

        int price = gameObject.GetComponent<Machine>().price;
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

    public void ShowBuyScreen(GameObject gameObject)
    {
        if (gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.Active)) return;
        // * Accessing the Canvas with GetChild(0)
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<Machine>().buyScreenStatus = BuyScreenStatus.Active;
    }
    public void CloseBuyScreen(GameObject gameObject)
    {
        if (gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.InActive)) return;

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Machine>().buyScreenStatus = BuyScreenStatus.InActive;

    }

    public void Reset()
    {
        popcornMachineUnlocked = false;
        nachosMachineUnlocked = false;
        sodaMachineUnlocked = false;
    }

}
