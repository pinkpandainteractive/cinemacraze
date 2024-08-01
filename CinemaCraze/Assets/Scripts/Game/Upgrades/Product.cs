using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ProductData;


public class Product : MonoBehaviour
{
    public ProductionStatus productionStatus;
    public CapacityStatus capacityStatus;
    public RefillStatus refillStatus;
    
    public Button btn_refill;
    public float productionTime;
    public int productionLevel;
    public float refillTime;
    public int refillLevel;
    public int maxCapacity;
    public int maxCapacityLevel;
    public int capacity;

    public TMP_Text txt_machineEmpty;
    public TMP_Text txt_cap;
    public Inventory inventory;
    public Progressbar progressbar;
    private Material mtl_default;

    
    private Coroutine refillCoroutine;


    private float defaultProductionTime;
    private int defaultProductionLevel;
    private float defaultRefillTime;
    private int defaultRefillLevel;
    private int defaultMaxCapacity;
    private int defaultMaxCapacityLevel;
    private int defaultCapacity;
public ProductData GetProductData()
    {
        return new ProductData
        {
            productionStatus = this.productionStatus,
            capacityStatus = this.capacityStatus,
            refillStatus = this.refillStatus,
            productionTime = this.productionTime,
            productionLevel = this.productionLevel,
            refillTime = this.refillTime,
            refillLevel = this.refillLevel,
            maxCapacity = this.maxCapacity,
            maxCapacityLevel = this.maxCapacityLevel,
            capacity = this.capacity
        };
    }

    public void SetProductData(ProductData data)
    {
        this.productionStatus = data.productionStatus;
        this.capacityStatus = data.capacityStatus;
        this.refillStatus = data.refillStatus;
        this.productionTime = data.productionTime;
        this.productionLevel = data.productionLevel;
        this.refillTime = data.refillTime;
        this.refillLevel = data.refillLevel;
        this.maxCapacity = data.maxCapacity;
        this.maxCapacityLevel = data.maxCapacityLevel;
        this.capacity = data.capacity;
    }
    void Start()
    {
        
        defaultProductionTime = productionTime;
        defaultProductionLevel = productionLevel;
        defaultRefillTime = refillTime;
        defaultRefillLevel = refillLevel;
        defaultMaxCapacity = maxCapacity;
        defaultMaxCapacityLevel = maxCapacityLevel;
        defaultCapacity = maxCapacity;

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
        refillCoroutine = StartCoroutine(RefillCapacity());
        StartCoroutine(progressbar.SliderProgress(refillTime));
    }
    void StopRefill()
    {
        if(refillCoroutine != null){
        StopCoroutine(RefillCapacity());
        refillCoroutine = null;
        }
        progressbar.ResetProgressbar();
        refillStatus = RefillStatus.None;
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
    public void ResetProduct()
    {
       
        productionTime = defaultProductionTime;
        productionLevel = defaultProductionLevel;
        refillTime = defaultRefillTime;
        refillLevel = defaultRefillLevel;
        maxCapacity = defaultMaxCapacity;
        maxCapacityLevel = defaultMaxCapacityLevel;
        capacity = defaultCapacity;
        txt_cap.text = capacity.ToString() + " / " + maxCapacity;
        productionStatus = ProductionStatus.None;
        capacityStatus = CapacityStatus.Full;
        refillStatus = RefillStatus.None;
        StopRefill();
    }
    public void LoadProducts(GameData gameData, int index){
        
        SetProductData(gameData.products[index]);
        
    }
   
   
}
