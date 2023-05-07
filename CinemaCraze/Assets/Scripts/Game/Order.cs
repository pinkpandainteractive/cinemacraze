using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
   
    public Text order;
    public MainMenu mainMenu;
    public List<string> GenerateOrder()
    {
        List<string> listOrder = new List<string>();
        if (mainMenu != null)
        {
            if (!mainMenu.mainMenuUI.activeInHierarchy)
            {
                if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numPopcorn = Random.Range(1, 4);
                    order.text = "Popcorn x" + numPopcorn;
                    listOrder.Add("Popcorn x" + numPopcorn);
                    return listOrder;
                }
                else if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 4);
                    order.text = "Nacho x" + numNacho;
                    listOrder.Add("Nacho x" + numNacho);
                    return listOrder;
                }
                else
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 3);
                    int numPopcorn = Random.Range(1, 3);
                    order.text = "Nacho x" + numNacho + ", Popcorn x" + numPopcorn;

                    listOrder.Add("Nacho x" + numNacho);
                    listOrder.Add("Popcorn x" + numPopcorn);
                    listOrder.Sort();
                    return listOrder;
                }
            }
        }
        return listOrder;
    }

    public void DeleteOrder(List<string> listOrder)
    {
        listOrder.Clear();
        order.text = "";
    }
}
