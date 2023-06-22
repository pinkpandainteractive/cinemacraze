using UnityEngine;
[System.Serializable]
public class CustomerOrder
{
    const int NUMBER_OF_ORDER_TYPES = 2;
    const int NUMBER_OF_ORDER_VARIATIONS = 5;

    public int popcorn { get; set;}
    public int nachos { get; set; }
    public int soda { get; set; }

    public CustomerOrder()
    {
        popcorn = 0;
        nachos = 0;
        soda = 0;
        GenerateOrder();
    }

    void GenerateOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_TYPES);

        switch(random)
        {
            case 1:
                GenerateSimpleOrder();
                break;
            case 2:
                GenerateComplexOrder();
                break;
        }
    }

    void GenerateSimpleOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS);

        switch(random)
        {
            case 1:
                popcorn = Random.Range(0, 1);
                soda = Random.Range(1, 2);
                nachos = Random.Range(0, 1);
                break;
            case 2:
                popcorn = Random.Range(0, 1);
                soda = Random.Range(0, 1);
                nachos = Random.Range(1, 2);
                break;
            case 3:
                popcorn = Random.Range(1, 2);
                soda = Random.Range(0, 1);
                nachos = Random.Range(0, 1);
                break;
            case 4:
                popcorn = Random.Range(2, 3);
                soda = Random.Range(0, 1);
                break;
            case 5:
                popcorn = Random.Range(0, 1);
                soda = Random.Range(0, 1);
                nachos = Random.Range(0, 1);
                break;
        }
    }

    void GenerateComplexOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS);

        switch(random)
        {
            case 1:
                popcorn = Random.Range(2, 5);
                soda = Random.Range(1, 3);
                nachos = Random.Range(1, 3);
                break;
            case 2:
                popcorn = Random.Range(1, 3);
                soda = Random.Range(1, 3);
                nachos = Random.Range(2, 5);
                break;
            case 3:
                popcorn = Random.Range(1, 2);
                nachos = Random.Range(2, 4);
                soda = Random.Range(4, 5);
                break;
            case 4:
                popcorn = Random.Range(2, 4);
                nachos = Random.Range(1, 2);
                soda = Random.Range(4, 5);
                break;
            case 5:
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    popcorn = Random.Range(8, 10);
                    nachos = Random.Range(0, 1);
                    soda = Random.Range(0, 1);
                }
                else if (random == 2)
                {
                    popcorn = Random.Range(0, 1);
                    nachos = Random.Range(8, 10);
                    soda = Random.Range(0, 1);
                }
                else if (random == 3)
                {
                    popcorn = Random.Range(0, 1);
                    nachos = Random.Range(0, 1);
                    soda = Random.Range(8, 10);
                }
                break;
        }
    }

}
