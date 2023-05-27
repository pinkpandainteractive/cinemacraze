
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
   
    public MainMenu mainMenu;
    
    public List<string> GenerateOrder()
    {
        List<string> listOrder = new ();
               
                if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numPopcorn = Random.Range(1, 4);
                    listOrder.Add("Popcorn x" + numPopcorn);
                    return listOrder;
                }
                else if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 4);
                    listOrder.Add("Nacho x" + numNacho);
                    return listOrder;
                }
                else if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 3);
                    int numPopcorn = Random.Range(1, 3);

                    listOrder.Add("Nacho x" + numNacho);
                    listOrder.Add("Popcorn x" + numPopcorn);
                    listOrder.Sort();
                    return listOrder;
                }
                else if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numSoda = Random.Range(1, 3);
                    int numPopcorn = Random.Range(1, 3);

                    listOrder.Add("Soda x" + numSoda);
                    listOrder.Add("Popcorn x" + numPopcorn);
                    listOrder.Sort();
                    return listOrder;
                }
                else if (Random.Range(0, 10) > 5)
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 3);
                    int numSoda = Random.Range(1, 3);

                    listOrder.Add("Nacho x" + numNacho);
                    listOrder.Add("Soda x" + numSoda);
                    listOrder.Sort();
                    return listOrder;
                }
                else
                {
                    listOrder.Clear();
                    int numNacho = Random.Range(1, 3);
                    int numPopcorn = Random.Range(1, 3);
                    int numSoda = Random.Range(1, 3);
                    listOrder.Add("Nacho x" + numNacho);
                    listOrder.Add("Popcorn x" + numPopcorn);
                    listOrder.Add("Soda x" + numSoda);
                    listOrder.Sort();
                    return listOrder;
                }
    }
    
}
