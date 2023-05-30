using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    public int nPopcorn;
    public int nNachos;
    public int nSoda;

    public TMP_Text orderText;

    void Init()
    {
        nPopcorn = 0;
        nNachos = 0;
        nSoda = 0;
        orderText.text = "";
    }

    public void GenerateOrder(float seed)
    {
        Init();
        if (seed < 0.5f) GenerateSimpleOrder(seed);
        else GenerateComplexOrder(seed);
        UpdateOrderText();
    }

    void GenerateSimpleOrder(float seed)
    {
        Debug.Log("Generating simple order");
        if (seed < 0.1f) GenerateSimpleOrder1();
        else if (seed < 0.2f) GenerateSimpleOrder2();
        else if (seed < 0.3f) GenerateSimpleOrder3();
        else if (seed < 0.4f) GenerateSimpleOrder4();
        else GenerateSimpleOrder5();
    }

    void GenerateSimpleOrder1()
    {
        nPopcorn = Random.Range(0, 1);
        nSoda = Random.Range(1, 2);
        nNachos = Random.Range(0, 1);
    }

    void GenerateSimpleOrder2()
    {
        nPopcorn = Random.Range(0, 1);
        nSoda = Random.Range(0, 1);
        nNachos = Random.Range(1, 2);
    }

    void GenerateSimpleOrder3()
    {
        nPopcorn = Random.Range(1, 2);
        nSoda = Random.Range(0, 1);
        nNachos = Random.Range(0, 1);
    }

    void GenerateSimpleOrder4()
    {
        nPopcorn = Random.Range(2, 3);
        nSoda = Random.Range(0, 1);
    }

    void GenerateSimpleOrder5()
    {
        nNachos = Random.Range(2, 3);
        nSoda = Random.Range(0, 1);
    }

    void GenerateComplexOrder(float seed)
    {
        Debug.Log("Generating complex order");
        if (seed > 0.9f) GenerateComplexOrder1();
        else if (seed > 0.8f) GenerateComplexOrder2();
        else if (seed > 0.7f) GenerateComplexOrder3();
        else if (seed > 0.6f) GenerateComplexOrder4();
        else GenerateComplexOrder5();
    }

    void GenerateComplexOrder1()
    {
        nPopcorn = Random.Range(2, 5);
        nSoda = Random.Range(1, 3);
        nNachos = Random.Range(1, 3);
    }

    void GenerateComplexOrder2()
    {
        nPopcorn = Random.Range(1, 3);
        nSoda = Random.Range(1, 3);
        nNachos = Random.Range(2, 5);
    }

    void GenerateComplexOrder3()
    {
        nPopcorn = Random.Range(1, 2);
        nNachos = Random.Range(2, 4);
        nSoda = Random.Range(4, 5);
    }

    void GenerateComplexOrder4()
    {
        nPopcorn = Random.Range(2, 4);
        nNachos = Random.Range(1, 2);
        nSoda = Random.Range(4, 5);
    }

    void GenerateComplexOrder5()
    {
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            nPopcorn = Random.Range(8, 10);
            nNachos = Random.Range(0, 1);
            nSoda = Random.Range(0, 1);
        }
        else if (random == 2)
        {
            nPopcorn = Random.Range(0, 1);
            nNachos = Random.Range(8, 10);
            nSoda = Random.Range(0, 1);
        }
        else if (random == 3)
        {
            nPopcorn = Random.Range(0, 1);
            nNachos = Random.Range(0, 1);
            nSoda = Random.Range(8, 10);
        }
    }

    void UpdateOrderText()
    {
        string textPopcorn = "";
        string textNachos = "";
        string textSoda = "";

        if (nPopcorn > 0) textPopcorn = "Popcorn:\t" + nPopcorn + "\n";
        if (nNachos > 0) textNachos = "Nachos:\t" + nNachos + "\n";
        if (nSoda > 0) textSoda = "Soda:\t" + nSoda + "\n";

        orderText.text = textPopcorn + textNachos + textSoda;
    }

    // ! Deprecated
    [System.Obsolete("XGenerateOrder is deprecated, please use GenerateOrder instead.")]
    public List<string> XGenerateOrder()
    {
        List<string> listOrder = new List<string>();

        float currentTime = Time.time; // Aktuelle Zeit seit dem Start des Spiels in Sekunden

        if (currentTime < 120f) // Zeitabh�ngigkeit f�r die ersten zwei Minuten
        {
            // Einfache Bestellungen h�ufiger
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
            // Komplexere Bestellungen h�ufiger
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
