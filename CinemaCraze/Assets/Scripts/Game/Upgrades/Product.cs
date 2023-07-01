using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Product : MonoBehaviour
{
    public ProductionStatus productionStatus;
    public float productionTime = 3.0f;
    public Inventory inventory;
    public Progressbar progressbar;
    private Material mtl_default;

    // Start is called before the first frame update
    void Start()
    {
        mtl_default = gameObject.GetComponent<Renderer>().material;
        productionStatus = ProductionStatus.None;
    }

    void Update()
    {
        progressbar.SliderProgress(productionTime);
    }

    public IEnumerator ProductionTimer(string tag)
    {
        yield return new WaitForSeconds(productionTime);

        if (tag.Equals("Popcorn"))
        {
            inventory.AddPopcorn(1);
        }
        else if (tag.Equals("Nachos"))
        {
            inventory.AddNachos(1);
        }
        else if (tag.Equals("Soda"))
        {
            inventory.AddSoda(1);
        }

        // * Reset button material to default
        gameObject.GetComponent<Renderer>().material = mtl_default;
        // * Reset progress bar
        progressbar.ResetProgressbar();

        productionStatus = ProductionStatus.Done;
    }
    
    public enum ProductionStatus
    {
        None,
        Waiting,
        Done
    }
}
