using Unity.VisualScripting;
using UnityEngine;
using static Machine;

public class MachineManager : MonoBehaviour
{

    public Score score;
    public Material matl_colorPalette;
    public Material mtl_locked;
    public Material mtl_hover;

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
        gameObject.GetComponent<Machine>().machineStatus = MachineStatus.Owned;

        gameObject.GetComponent<Machine>().product.SetActive(true);

        gameObject.GetComponent<Renderer>().material = matl_colorPalette;
        score.SubtractScore(price);
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
            ShowBuyScreen(gameObject);
        }
        else
        {
            CloseBuyScreen(gameObject);
        }
    }

    public void ShowBuyScreen(GameObject gameObject)
    {
        if(gameObject.GetComponent<Machine>().buyScreenStatus.Equals(BuyScreenStatus.Active)) return;
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

}
