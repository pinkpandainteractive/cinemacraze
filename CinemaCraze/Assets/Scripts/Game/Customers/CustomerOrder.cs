using UnityEngine;
[System.Serializable]
public class CustomerOrder
{
    const int NUMBER_OF_ORDER_VARIATIONS = 5;

    public OrderStatus status { get; set; }

    public int value { get; set; }

    public int popcorn { get; set; }
    public int nachos { get; set; }
    public int soda { get; set; }

    public float timeRemaining { get; set; }

    public CustomerOrder()
    {
        status = OrderStatus.Undefined;

        value = 0;

        popcorn = 0;
        nachos = 0;
        soda = 0;

        timeRemaining = 0f;
    }

    public void GenerateOrder()
    {
        float random = Random.Range(0, 1f);
        Debug.Log("Random: " + random);

        if (random < 0.80f)
        {
            GenerateSimpleOrder();
            value = 5 * Random.Range(1, 4);
        }
        else
        {
            GenerateComplexOrder();
            value = 5 * Random.Range(4, 10);
        }

        this.status = OrderStatus.Ordering;
        Debug.Log("Order generated: " + popcorn + " popcorn, " + soda + " soda, " + nachos + " nachos");

    }

    void GenerateSimpleOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS + 1);
        Debug.Log("Simple order: " + random);
        switch (random)
        {
            case 1:
                popcorn = Random.Range(0, 2);
                soda = Random.Range(2, 3);
                nachos = Random.Range(0, 2);
                break;
            case 2:
                popcorn = Random.Range(0, 2);
                soda = Random.Range(0, 2);
                nachos = Random.Range(2, 3);
                break;
            case 3:
                popcorn = Random.Range(2, 3);
                soda = Random.Range(0, 2);
                nachos = Random.Range(0, 2);
                break;
            case 4:
                popcorn = Random.Range(3, 4);
                soda = Random.Range(0, 2);
                break;
            case 5:
                popcorn = Random.Range(0, 2);
                soda = Random.Range(0, 2);
                nachos = Random.Range(1, 2);
                break;
        }

        timeRemaining = Random.Range(10f, 15f);

    }

    void GenerateComplexOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS + 1);
        Debug.Log("Complex order variation: " + random);
        switch (random)
        {
            case 1:
                popcorn = Random.Range(3, 6);
                soda = Random.Range(2, 4);
                nachos = Random.Range(2, 4);
                break;
            case 2:
                popcorn = Random.Range(2, 4);
                soda = Random.Range(2, 4);
                nachos = Random.Range(3, 6);
                break;
            case 3:
                popcorn = Random.Range(2, 3);
                nachos = Random.Range(3, 5);
                soda = Random.Range(5, 6);
                break;
            case 4:
                popcorn = Random.Range(3, 5);
                nachos = Random.Range(2, 3);
                soda = Random.Range(5, 6);
                break;
            case 5:
                random = Random.Range(0, 4);
                if (random == 1)
                {
                    popcorn = Random.Range(6, 9);
                    nachos = Random.Range(0, 2);
                    soda = Random.Range(0, 2);
                }
                else if (random == 2)
                {
                    popcorn = Random.Range(0, 2);
                    nachos = Random.Range(6, 9);
                    soda = Random.Range(0, 2);
                }
                else if (random == 3)
                {
                    popcorn = Random.Range(0, 2);
                    nachos = Random.Range(0, 2);
                    soda = Random.Range(6, 9);
                }
                break;
        }

        timeRemaining = Random.Range(15f, 25f);
    }

    public override string ToString()
    {
        string order = "";

        if (status == OrderStatus.Ordering)
        {
            if (popcorn > 0)
            {
                order += popcorn + " popcorn";
            }

            if (soda > 0)
            {
                if (order.Length > 0)
                {
                    order += "\n";
                }
                order += soda + " soda";
            }

            if (nachos > 0)
            {
                if (order.Length > 0)
                {
                    order += "\n";
                }
                order += nachos + " nachos";
            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                order += "\n" + timeRemaining.ToString("0.0") + " s";
            }
            else
            {
                status = OrderStatus.Failed;
            }
        }

        return order;
    }

}
