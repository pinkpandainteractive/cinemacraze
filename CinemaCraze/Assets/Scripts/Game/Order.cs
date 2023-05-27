
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
   
    public MainMenu mainMenu;

    public List<string> GenerateOrder()
    {
        List<string> listOrder = new List<string>();

        float currentTime = Time.time; // Aktuelle Zeit seit dem Start des Spiels in Sekunden

        if (currentTime < 120f) // Zeitabhängigkeit für die ersten zwei Minuten
        {
            // Einfache Bestellungen häufiger
            if (Random.Range(0, 10) > 5)
            {
                int numPopcorn = Random.Range(1, 4);
                listOrder.Add("Popcorn x" + numPopcorn);
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 4);
                listOrder.Add("Nacho x" + numNacho);
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numSoda = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 3);
                int numSoda = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Sort();
            }
            else
            {
                int numNacho = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                int numSoda = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Sort();
            }
        }
        else
        {
            // Komplexere Bestellungen häufiger
            if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                int numSoda = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 3);
                int numSoda = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numSoda = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                listOrder.Add("Soda x" + numSoda);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 3);
                int numPopcorn = Random.Range(1, 3);
                listOrder.Add("Nacho x" + numNacho);
                listOrder.Add("Popcorn x" + numPopcorn);
                listOrder.Sort();
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numNacho = Random.Range(1, 4);
                listOrder.Add("Nacho x" + numNacho);
            }
            else if (Random.Range(0, 10) > 5)
            {
                int numPopcorn = Random.Range(1, 4);
                listOrder.Add("Popcorn x" + numPopcorn);
            }
        }

        return listOrder;
    }

    

}
