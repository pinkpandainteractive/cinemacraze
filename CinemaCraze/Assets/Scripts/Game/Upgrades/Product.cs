using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    public ProductionStatus productionStatus;
    public CapacityStatus capacityStatus;
    public RefillStatus refillStatus;
    
    public Button btn_refill;
    public float productionTime = 3.0f;
    public float refillTime = 3.0f;
    public int maxCapacity = 10;
    public int capacity;

    public TMP_Text txt_machineEmpty;
    public TMP_Text txt_cap;
    public Inventory inventory;
    public Progressbar progressbar;
    private Material mtl_default;

    void Start()
    {
        capacity = maxCapacity;
        mtl_default = gameObject.GetComponent<Renderer>().material;
        btn_refill.onClick.AddListener(StartRefill);

        txt_cap.text = capacity.ToString()+" / "+maxCapacity;

        HandleEmptyVisibilty();
        productionStatus = ProductionStatus.None;
        capacityStatus = CapacityStatus.Full;
    }

    public IEnumerator ProductionTimer(string tag)
    {
        progressbar.ProgressbarColor(Color.green);
        StartCoroutine(progressbar.SliderProgress(productionTime));
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

        ReduceCapacity(1);
        txt_cap.text = capacity.ToString() + " / " + maxCapacity;
        // * Reset button material to default
        gameObject.GetComponent<Renderer>().material = mtl_default;
        // * Reset progress bar
        progressbar.ResetProgressbar();

        productionStatus = ProductionStatus.Done;
    }
    void HandleEmptyVisibilty()
    {
        if(txt_machineEmpty.isActiveAndEnabled || btn_refill.isActiveAndEnabled)
        {
            txt_machineEmpty.transform.gameObject.SetActive(false);
            btn_refill.transform.gameObject.SetActive(false);
        }
        else
        {
            txt_machineEmpty.transform.gameObject.SetActive(true);
            btn_refill.transform.gameObject.SetActive(true);
        }
        
    }
    int ReduceCapacity(int amount)
    {
        if (capacity > 1)
        {
            capacityStatus = CapacityStatus.Available;
            return capacity -= amount;
        }
        capacityStatus = CapacityStatus.Empty;
        HandleEmptyVisibilty();
        return 0;
    }
    void StartRefill()
    {
        if (refillStatus == RefillStatus.Filling) return;

        progressbar.ProgressbarColor(Color.red);
        StartCoroutine(RefillCapacity());
        StartCoroutine(progressbar.SliderProgress(refillTime));
    }
    
    IEnumerator RefillCapacity()
    {
        progressbar.HandleVisbilityVisible();
        // * Start progess bar
        gameObject.transform.GetChild(0).GetComponent<Progressbar>().IncrementProgress(1.0f);
        refillStatus  = RefillStatus.Filling;
        yield return new WaitForSeconds(refillTime);

        progressbar.ResetProgressbar();
        capacity = maxCapacity;
        capacityStatus = CapacityStatus.Full;
        refillStatus = RefillStatus.Done;
        HandleEmptyVisibilty();
    }
   
    public enum ProductionStatus
    {
        None,
        Waiting,
        Done
    }
    public enum RefillStatus
    {
        None,
        Filling,
        Done
    }
    public enum CapacityStatus
    {
        Empty,
        Available,
        Full
    }
}
