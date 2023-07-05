using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    private ProductionUpgradeStatus productionLevel1UpgradeStatus;
    private ProductionUpgradeStatus productionLevel2UpgradeStatus;
    private ProductionUpgradeStatus productionLevel3UpgradeStatus;
    private RefillLevelUpgradeStatus refillLevel1UpgradeStatus;
    private RefillLevelUpgradeStatus refillLevel2UpgradeStatus;
    private RefillLevelUpgradeStatus refillLevel3UpgradeStatus;
    private CapacityLevelUpgradeStatus capacityLevel1UpgradeStatus;
    private CapacityLevelUpgradeStatus capacityLevel2UpgradeStatus;
    private CapacityLevelUpgradeStatus capacityLevel3UpgradeStatus;
    public Score score;
    public AudioHandler audioHandler;
    private Color basic = Color.white;
    private Color invalid = new(1.0f, 0.3f, 0.3f, 1.0f);
    private Color finished = new(1.0f, 0.31f, 0.58f, 1.0f);
    //private Color lvl1 = new(0.0f, 1.0f, 0.3f, 1.0f);
    private Color lvl2 = new(0.0f, 0.2f, 1.0f, 1.0f);
    private Color lvl3 = new(1.0f,1.0f,0,1.0f);
    public GameObject product;
    public Button btnRefillSpeed;
    public Button btnProductionSpeed;
    public Button btnCapacity;
    public TMP_Text capacityText;
    public TMP_Text refillText;
    public TMP_Text productionText;
    [Header("Capacity upgrades")]
    public int capacityLevel1 = 20;
    public int priceCapacityLevel1 = 10000;
    public int capacityLevel2 = 45;
    public int priceCapacityLevel2 = 25000;
    public int capacityLevel3 = 100;
    public int priceCapacityLevel3 = 50000;
    [Header("Production speed upgrade")]
    public float productionSpeedLevel1 = 0.5f;
    public int priceProductionSpeedLevel1 = 25000;
    public float productionSpeedLevel2 = 0.66f;
    public int priceProductionSpeedLevel2 = 25000;
    public float productionSpeedLevel3 = 0.0f;
    public int priceProductionSpeedLevel3 = 100000;
    [Header("Refill speed upgrades")]
    public float refillSpeedLevel1 = 0.5f;
    public int priceRefillSpeedLevel1 = 1000;
    public float refillSpeedLevel2 = 0.66f;
    public int priceRefillSpeedLevel2 = 5000;
    public float refillSpeedLevel3 = 0.0f;
    public int priceRefillSpeedLevel3 = 12500;

    void Start()
    {
        InitPrice();
        InitText(capacityLevel1,refillSpeedLevel1,productionSpeedLevel1);

        btnProductionSpeed.onClick.AddListener(delegate {UpgradeProduction(product, btnProductionSpeed); });
        btnRefillSpeed.onClick.AddListener(delegate { UpgradeRefill(product, btnRefillSpeed); });
        btnCapacity.onClick.AddListener(delegate {UpgradeCapacity(product, btnCapacity); });
    }
    public void UpgradeProduction(GameObject obj, Button btn)
    {
        if (productionLevel3UpgradeStatus.Equals(ProductionUpgradeStatus.Done)) return;
        if (score.GetScore() < NewProdPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        obj.GetComponent<Product>().productionTime *= NewProdLevel();
        CreateNewProductionLevel();
        audioHandler.PlayBuyUpgrades();
        if (productionLevel3UpgradeStatus.Equals(ProductionUpgradeStatus.Done))
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }
        
        btn.GetComponentInChildren<TMP_Text>().text = $"{NewProdPriceLevel()}" + " $";
        UpdateTextProduction(NewProdLevel());
    }
    public void UpgradeRefill(GameObject obj, Button btn)
    {
        if (refillLevel3UpgradeStatus.Equals(RefillLevelUpgradeStatus.Done)) return;


        if (score.GetScore() < NewRefillPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        obj.GetComponent<Product>().refillTime *= NewRefillLevel();
        CreateNewRefillLevel();
        audioHandler.PlayBuyUpgrades();
        if (refillLevel3UpgradeStatus.Equals(RefillLevelUpgradeStatus.Done))
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }

        btn.GetComponentInChildren<TMP_Text>().text = $"{NewRefillPriceLevel()}" + " $";
        UpdateTextRefill( NewRefillLevel());
    }
    public void UpgradeCapacity(GameObject obj, Button btn)
    {
        // * All upgrades done
        if (capacityLevel3UpgradeStatus.Equals(CapacityLevelUpgradeStatus.Done)) return;
        
        if (score.GetScore() < NewCapPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        obj.GetComponent<Product>().capacity = NewCapLevel();
        obj.GetComponent<Product>().maxCapacity = NewCapLevel();
        CreateNewCapLevel();
        audioHandler.PlayBuyUpgrades();
        if (capacityLevel3UpgradeStatus.Equals(CapacityLevelUpgradeStatus.Done))
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }
        btn.GetComponentInChildren<TMP_Text>().text = $"{NewCapPriceLevel()}"+" $";
        UpdateTextCapacity(NewCapLevel());
    }

    void UpdateTextRefill( float value)
    {
        string speed = "erhöhen";
        if (value == 0.5f)
        {
            speed = "verdoppeln";
        }
        else if (value == 0.66f)
        {
            speed = "verdreifachen";
        }
        else if (value == 0.0f)
        {
            speed = "sofort";
        }
        
        refillText.text = "Auffüll-geschwindigkeit " + speed; 
    }
    void UpdateTextProduction(float value )
    {
        string speed = "erhöhen";
        if (value == 0.5f)
        {
            speed = "verdoppeln";
        }
        else if (value == 0.66f)
        {
            speed = "verdreifachen";
        }
        else if (value == 0.0f)
        {
            speed = "sofort";
        }

        productionText.text = "Produktions-geschwindigkeit " + speed;
    }

    void UpdateTextCapacity(int cap)
    {   
        capacityText.text = "Kapazität auf " + cap.ToString() + " erhöhen"; 
    }
    void InitText(int value, float speed, float prodSpeed)
    {
        capacityText.text = "Kapazität auf " + value.ToString() + " erhöhen";
        UpdateTextRefill(speed);
        UpdateTextProduction(prodSpeed);
    }
    void InitPrice()
    {
        btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceProductionSpeedLevel1}" + " $";
        btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceRefillSpeedLevel1}" + " $";
        btnCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceCapacityLevel1}" + " $";
    }

    void CreateNewCapLevel()
    {
        if (capacityLevel1UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            capacityLevel1UpgradeStatus = CapacityLevelUpgradeStatus.Done;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "2";
        }
        else
        if (capacityLevel2UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            capacityLevel2UpgradeStatus = CapacityLevelUpgradeStatus.Done;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl3;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (capacityLevel3UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            capacityLevel3UpgradeStatus = CapacityLevelUpgradeStatus.Done;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    void CreateNewRefillLevel()
    {
        if (refillLevel1UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            refillLevel1UpgradeStatus = RefillLevelUpgradeStatus.Done;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "2";
        }
        else
        if (refillLevel2UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            refillLevel2UpgradeStatus = RefillLevelUpgradeStatus.Done;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl3;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (refillLevel3UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            refillLevel3UpgradeStatus = RefillLevelUpgradeStatus.Done;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    void CreateNewProductionLevel()
    {
        if (productionLevel1UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            productionLevel1UpgradeStatus = ProductionUpgradeStatus.Done;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "2";
        }
        else
        if (productionLevel2UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            productionLevel2UpgradeStatus = ProductionUpgradeStatus.Done;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl3;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (productionLevel3UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            productionLevel3UpgradeStatus = ProductionUpgradeStatus.Done;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    int NewCapLevel()
    {
        if (capacityLevel1UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return capacityLevel1;
        }
        else
        if (capacityLevel2UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return capacityLevel2;
        }
        else
        if (capacityLevel3UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return capacityLevel3;
        }
        return 0;
    }
    float NewRefillLevel()
    {
        if (refillLevel1UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return refillSpeedLevel1;
        }
        else
        if (refillLevel2UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return refillSpeedLevel2;
        }
        else
        if (refillLevel3UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return refillSpeedLevel3;
        }
        return 0;
    }
    float NewProdLevel()
    {
        if (productionLevel1UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return productionSpeedLevel1;
        }
        else
        if (productionLevel2UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return productionSpeedLevel2;
        }
        else
        if (productionLevel3UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return productionSpeedLevel3;
        }
        return 0;
    }
    int NewRefillPriceLevel()
    {
        if (refillLevel1UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return priceRefillSpeedLevel1;
        }
        else
        if (refillLevel2UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return priceRefillSpeedLevel2;
        }
        else
        if (refillLevel3UpgradeStatus.Equals(RefillLevelUpgradeStatus.None))
        {
            return priceRefillSpeedLevel3;
        }
        return 0;
    }
    int NewCapPriceLevel()
    {
        if (capacityLevel1UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return priceCapacityLevel1;
        }
        else
        if (capacityLevel2UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return priceCapacityLevel2;
        }
        else
        if (capacityLevel3UpgradeStatus.Equals(CapacityLevelUpgradeStatus.None))
        {
            return priceCapacityLevel3;
        }
        return 0;
    }
    int NewProdPriceLevel()
    {
        if (productionLevel1UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return priceProductionSpeedLevel1;
        }
        else
        if (productionLevel2UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return priceProductionSpeedLevel2;
        }
        else
        if (productionLevel3UpgradeStatus.Equals(ProductionUpgradeStatus.None))
        {
            return priceProductionSpeedLevel3;
        }
        return 0;
    }

    IEnumerator ChangeButtonColorForSec(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.GetComponent<Image>().color = basic;
    }
    // * Upgrade enums
    public enum ProductionUpgradeStatus
    {
        None,
        Done
    }
    public enum RefillLevelUpgradeStatus
    {
        None,
        Done
    }
    
    public enum CapacityLevelUpgradeStatus
    {
        None,
        Done
    }
    
}
