using UnityEngine;
using UnityEngine.UI;
using static Product;

public class ProductManager : MonoBehaviour
{
    public Material mtl_glow;
    public void StartTimer(string tag, GameObject gameObject)
    {
        if (gameObject.GetComponent<Product>().productionStatus == ProductionStatus.Waiting) return;

        gameObject.GetComponent<Renderer>().material = mtl_glow;
        // * First GetChild(0) for Canvas second GetChild(0) for progessbar
        gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
       
        gameObject.GetComponent<Product>().productionStatus = ProductionStatus.Waiting;

        // * Start progess bar
        gameObject.transform.GetChild(0).GetComponent<Progressbar>().IncrementProgress(1.0f);
        // * Start production
        StartCoroutine(gameObject.GetComponent<Product>().ProductionTimer(tag));
    }
}
