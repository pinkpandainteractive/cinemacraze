using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Product;
using static ProductData;


public class ProductManager : MonoBehaviour
{
    public Material mtl_glow;
    
    public Product popcorn;
    public Product nachos;
    public Product soda;
    private Coroutine productionCoroutine;
    public void StartTimer(string tag, GameObject gameObject)
    {
        if (gameObject.GetComponent<Product>().productionStatus == ProductionStatus.Waiting) return;
        if (gameObject.GetComponent<Product>().capacityStatus == CapacityStatus.Empty) return;


        gameObject.GetComponent<Renderer>().material = mtl_glow;
        // * First GetChild(0) for Canvas second GetChild(0) for progessbar
        gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
       
        gameObject.GetComponent<Product>().productionStatus = ProductionStatus.Waiting;

        // * Start progess bar
        gameObject.transform.GetChild(0).GetComponent<Progressbar>().IncrementProgress(1.0f);
        // * Start production
        productionCoroutine = StartCoroutine(gameObject.GetComponent<Product>().ProductionTimer(tag));
        
    }
    public void LoadProducts(GameData gameData,String tag){
        
        switch (tag)
        {
            case "Popcorn":
                popcorn.LoadProducts(gameData,0);
                break;
            case "Nachos":
                nachos.LoadProducts(gameData,1);
                break;
            case "Soda":
                soda.LoadProducts(gameData,2);
                break;
        }
        
    }
    public void ResetProducts()
    {
        if(productionCoroutine != null){
        StopCoroutine(productionCoroutine);
        productionCoroutine = null;
        }
        popcorn.ResetProduct();
        nachos.ResetProduct();
        soda.ResetProduct();
    }
}
