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

    bool popcornUnlocked;
    bool sodaUnlocked;
    bool nachosUnlocked;

    public float timeRemaining { get; set; }

    public CustomerOrder()
    {
        status = OrderStatus.Undefined;

        value = 0;

        popcorn = 0;
        nachos = 0;
        soda = 0;

        this.popcornUnlocked = false;
        this.sodaUnlocked = false;
        this.nachosUnlocked = false;

        timeRemaining = 0f;
    }

   
    public void GenerateOrder(bool popcornUnlocked, bool sodaUnlocked, bool nachosUnlocked)
    {

        this.popcornUnlocked = popcornUnlocked;
        this.sodaUnlocked = sodaUnlocked;
        this.nachosUnlocked = nachosUnlocked;
        
        float random = Random.Range(0, 1f);
        /* Debug.Log("Random: " + random); */

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
        /* Debug.Log("Order generated: " + popcorn + " popcorn, " + soda + " soda, " + nachos + " nachos"); */

    }

    void GenerateSimpleOrder()
    {
        int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS + 1);
      
        switch (random)
        {
            case 1:
                popcorn = popcornUnlocked ? Random.Range(0, 2) : 0;
                soda =  sodaUnlocked ? Random.Range(2, 3) : 0;
                nachos = nachosUnlocked ? Random.Range(0, 2) : 0;
                break;
            case 2:
                popcorn = popcornUnlocked ? Random.Range(0, 2):0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
                nachos = nachosUnlocked ? Random.Range(2, 3): 0;
                break;
            case 3:
                popcorn = popcornUnlocked ? Random.Range(2, 3) : 0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
                nachos = nachosUnlocked ? Random.Range(0, 2) : 0;
                break;
            case 4:
                popcorn = popcornUnlocked ? Random.Range(3, 4) : 0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
                break;
            case 5:
                popcorn = popcornUnlocked ? Random.Range(0, 2) : 0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
                nachos = nachosUnlocked ? Random.Range(1, 2) : 0;
                break;
        }
        if(popcorn==0 && soda==0 && nachos==0)
        {
            GenerateSimpleOrder();
        }
        Debug.Log("Simple order: " + popcorn + " popcorn, " + soda + " soda, " + nachos + " nachos"+" popcornUnlocked: "+!popcornUnlocked);
       

        timeRemaining = Random.Range(10f, 15f);

    }

void GenerateComplexOrder()
{
    int random = Random.Range(1, NUMBER_OF_ORDER_VARIATIONS);
    Debug.Log("Complex order variation: " + random);

    switch (random)
    {
        case 1:
            popcorn = popcornUnlocked ? Random.Range(3, 6) : 0;
            soda = sodaUnlocked ? Random.Range(2, 4) : 0;
            nachos = nachosUnlocked ? Random.Range(2, 4) : 0;
            break;
        case 2:
            popcorn = popcornUnlocked ? Random.Range(2, 4) : 0;
            soda = sodaUnlocked ? Random.Range(2, 4) : 0;
            nachos = nachosUnlocked ? Random.Range(3, 6) : 0;
            break;
        case 3:
            popcorn = popcornUnlocked ? Random.Range(2, 3) : 0;
            nachos = nachosUnlocked ? Random.Range(3, 5) : 0;
            soda = sodaUnlocked ? Random.Range(5, 6) : 0;
            break;
        case 4:
            popcorn = popcornUnlocked ? Random.Range(3, 5) : 0;
            nachos = nachosUnlocked ? Random.Range(2, 3) : 0;
            soda = sodaUnlocked ? Random.Range(5, 6) : 0;
            break;
        case 5:
            int subRandom = Random.Range(0, 3);
            if (subRandom == 0)
            {
                popcorn = popcornUnlocked ? Random.Range(6, 9) : 0;
                nachos = nachosUnlocked ? Random.Range(0, 2) : 0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
            }
            else if (subRandom == 1)
            {
                popcorn = popcornUnlocked ? Random.Range(0, 2) : 0;
                nachos = nachosUnlocked ? Random.Range(6, 9) : 0;
                soda = sodaUnlocked ? Random.Range(0, 2) : 0;
            }
            else if (subRandom == 2)
            {
                popcorn = popcornUnlocked ? Random.Range(0, 2) : 0;
                nachos = nachosUnlocked ? Random.Range(0, 2) : 0;
                soda = sodaUnlocked ? Random.Range(6, 9) : 0;
            }
            break;
    }

    timeRemaining = Random.Range(22f, 35f);
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
