using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UpgradesData;
public class Upgrades : MonoBehaviour
{

    public Score score;
    public AudioHandler audioHandler;
    private Color basic = Color.white;
    private Color invalid = ProjectColors.LIGHT_RED;
    private Color finished = ProjectColors.LIGHT_GREEN;
    private Color lvl0 = ProjectColors.BLUE;
    private Color lvl1 = ProjectColors.GREEN;
    private Color lvl2 = ProjectColors.PINK;
    public GameObject product;
    public Button btnRefillSpeed;
    public Button btnProductionSpeed;
    public Button btnCapacity;
    public TMP_Text capacityText;
    public TMP_Text refillText;
    public TMP_Text productionText;
    [Header("Capacity upgrades")]
    public int capacityLevel1 = 20;
    public int priceCapacityLevel1 = 1;
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


    private int currentRefillSpeedLevel;
    private int currentProductionSpeedLevel;
    private int currentCapacityLevel;
    private int defaultCapacityPrice;
    private float defaultRefillPrice;
    private float defaultProductionPrice;

    public UpgradesData GetUpgradesData(){
        return new UpgradesData{
            currentCapacityLevel = this.currentCapacityLevel,
            currentProductionSpeedLevel = this.currentProductionSpeedLevel,
            currentRefillSpeedLevel = this.currentRefillSpeedLevel
        };
        }
    public void SetUpgradesData(UpgradesData upgradesData){
       
        this.currentCapacityLevel = upgradesData.currentCapacityLevel;
        this.currentProductionSpeedLevel = upgradesData.currentProductionSpeedLevel;
        this.currentRefillSpeedLevel = upgradesData.currentRefillSpeedLevel;
    }
    void Start()
    {
        
        InitPrice();
        defaultCapacityPrice = priceCapacityLevel1;
        defaultRefillPrice = priceRefillSpeedLevel1;
        defaultProductionPrice = priceProductionSpeedLevel1;
        if(product.tag.Equals("Popcorn")){
            Debug.Log("Popcorn");
            Debug.Log("PriceCAP: "+priceCapacityLevel2);
        }
        if(product.tag.Equals("Nachos")){
            Debug.Log("Nachos");
            Debug.Log("PriceCAP: "+priceCapacityLevel2);
        }
        if(product.tag.Equals("Soda")){
            Debug.Log("Soda");
            Debug.Log("PriceCAP: "+priceCapacityLevel2);
        }
        currentCapacityLevel = 0;
        currentProductionSpeedLevel = 0;
        currentRefillSpeedLevel = 0;
        InitText(capacityLevel1,refillSpeedLevel1,productionSpeedLevel1);
        
        btnProductionSpeed.onClick.AddListener(delegate {UpgradeProduction(product, btnProductionSpeed); });
        btnRefillSpeed.onClick.AddListener(delegate { UpgradeRefill(product, btnRefillSpeed); });
        btnCapacity.onClick.AddListener(delegate {UpgradeCapacity(product, btnCapacity); });
    }
    
    public void UpgradeProduction(GameObject obj, Button btn)
    {
        
        if (currentProductionSpeedLevel == 3) return;
        
        if (score.GetScore() < NewProdPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        Debug.Log("SCORE: " + score.GetScore() + " Subtract: " + NewProdPriceLevel());
        score.SubtractScore(NewProdPriceLevel());
        
         obj.GetComponent<Product>().productionTime *= NewProdLevel();
        obj.GetComponent<Product>().productionLevel++;
        currentProductionSpeedLevel++;
         btn.GetComponentInChildren<TMP_Text>().text = $"{NewProdPriceLevel()}" + " $";
        
        CreateNewProductionLevel();
        audioHandler.PlayBuyUpgrades();
        if (currentProductionSpeedLevel == 3)
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }
        
       
        UpdateTextProduction(NewProdLevel());
    }
    public void UpgradeRefill(GameObject obj, Button btn)
    {
        if (currentRefillSpeedLevel == 3) return;

        
        if (score.GetScore() < NewRefillPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        Debug.Log("SCORE: " + score.GetScore() + " Subtract: " + NewRefillPriceLevel());
        score.SubtractScore(NewRefillPriceLevel());
        
        obj.GetComponent<Product>().refillTime *= NewRefillLevel();
        obj.GetComponent<Product>().refillLevel++;
        currentRefillSpeedLevel++;
        btn.GetComponentInChildren<TMP_Text>().text = $"{NewRefillPriceLevel()}" + " $";
        
        CreateNewRefillLevel();
        audioHandler.PlayBuyUpgrades();
        if (currentRefillSpeedLevel == 3)
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }

        
        UpdateTextRefill( NewRefillLevel());
    }
    public void UpgradeCapacity(GameObject obj, Button btn)
    {
        // * All upgrades done
        if (currentCapacityLevel == 3) return;
        
        if (score.GetScore() < NewCapPriceLevel())
        {
            btn.GetComponent<Image>().color = invalid;
            audioHandler.PlayFail();
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }

        Debug.Log("SCORE: " + score.GetScore() + " Subtract: " + NewCapPriceLevel());
        score.SubtractScore(NewCapPriceLevel());
        
        obj.GetComponent<Product>().capacity = NewCapLevel();
        obj.GetComponent<Product>().maxCapacity = NewCapLevel();
        obj.GetComponent<Product>().maxCapacityLevel++;
        currentCapacityLevel++;
        btn.GetComponentInChildren<TMP_Text>().text = $"{NewCapPriceLevel()}"+" $";
        CreateNewCapLevel();
        audioHandler.PlayBuyUpgrades();
        
        if (currentCapacityLevel == 3)
        {
            btn.GetComponent<Image>().color = finished;
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "Fertig";
            return;
        }
        
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
        
        if (currentCapacityLevel == 1)
        {
           
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
        }
        else
        if (currentCapacityLevel == 2)
        {
           
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (currentCapacityLevel == 3)
        {
           
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    void CreateNewRefillLevel()
    {
        if (currentRefillSpeedLevel == 1)
        {
            
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
        }
        else
        if (currentRefillSpeedLevel == 2)
        {
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (currentRefillSpeedLevel == 3)
        {
            refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    void CreateNewProductionLevel()
    {
        if (currentProductionSpeedLevel == 1)
        {
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
        }
        else
        if (currentProductionSpeedLevel == 2)
        {
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
        }
        else
        if (currentProductionSpeedLevel == 3)
        {
            productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
    int NewCapLevel()
    {
        switch(currentCapacityLevel)
        {
            case 0:
                return capacityLevel1;
            case 1:
                return capacityLevel2;
            case 2:
                return capacityLevel3;
            default:
                return 0;
        }

    }
    float NewRefillLevel()
    {
        switch(currentRefillSpeedLevel)
        {
            case 0:
                return refillSpeedLevel1;
            case 1:
                return refillSpeedLevel2;
            case 2:
                return refillSpeedLevel3;
            default:
                return 0;
        }
        
    }
    float NewProdLevel()
    {
        switch(currentProductionSpeedLevel)
        {
            case 0:
                return productionSpeedLevel1;
            case 1:
                return productionSpeedLevel2;
            case 2:
                return productionSpeedLevel3;
            default:
                return 0;
        }
       
    }
    int NewRefillPriceLevel()
    {
        switch(currentRefillSpeedLevel)
        {
            case 0:
                return priceRefillSpeedLevel1;
            case 1:
                return priceRefillSpeedLevel2;
            case 2:
                return priceRefillSpeedLevel3;
            default:
                return 0;
        }
       
    }
    int NewCapPriceLevel()
    {
        switch(currentCapacityLevel)
        {
            case 0:
                return priceCapacityLevel1;
            case 1:
                return priceCapacityLevel2;
            case 2:
                return priceCapacityLevel3;
            default:
                return 0;
        }
   
       
    }
    int NewProdPriceLevel()
    {
        switch(currentProductionSpeedLevel)
        {
            case 0:
                return priceProductionSpeedLevel1;
            case 1:
                return priceProductionSpeedLevel2;
            case 2:
                return priceProductionSpeedLevel3;
            default:
                return 0;
        }
    }
public void ResetUpgrades()
    {       

        currentCapacityLevel = 0;
        currentProductionSpeedLevel = 0;
        currentRefillSpeedLevel = 0;

        btnCapacity.GetComponentInChildren<TMP_Text>().text = $"{defaultCapacityPrice}"+" $";
        btnCapacity.GetComponent<Image>().color = basic;
        btnCapacity.interactable = true;
        btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{defaultProductionPrice}"+" $";
        btnProductionSpeed.GetComponent<Image>().color = basic;
        btnProductionSpeed.interactable = true;
        btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{defaultRefillPrice}"+" $";
        btnRefillSpeed.GetComponent<Image>().color = basic;
        btnRefillSpeed.interactable = true;

        UpdateTextCapacity(capacityLevel1);
        UpdateTextRefill(refillSpeedLevel1);
        UpdateTextProduction(productionSpeedLevel1);

        productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
        productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
        refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
        refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
        capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
        capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
    }
    
    IEnumerator ChangeButtonColorForSec(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.GetComponent<Image>().color = basic;
    }
   
    public void LoadUpgrades(GameData gameData,int index){

        SetUpgradesData(gameData.upgrades[index]);

        switch(currentCapacityLevel){
            case 0:
                UpdateTextCapacity(capacityLevel1);
                btnCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceCapacityLevel1}"+" $";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
                break;
            case 1:
                UpdateTextCapacity(capacityLevel2);
                btnCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceCapacityLevel2}"+" $";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
                break;
            case 2:
                UpdateTextCapacity(capacityLevel3);
                btnCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceCapacityLevel3}"+" $";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
                capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
                break;
            case 3:
         
            btnCapacity.GetComponent<Image>().color = finished;
            btnCapacity.interactable = false;
            btnCapacity.GetComponentInChildren<TMP_Text>().text = "Fertig";
            capacityText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
            break;
        }
        
        
        switch(currentProductionSpeedLevel){
            case 0:
                UpdateTextProduction(productionSpeedLevel1);
                btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceProductionSpeedLevel1}"+" $";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
                break;
            case 1:
                UpdateTextProduction(productionSpeedLevel2);
                btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceProductionSpeedLevel2}"+" $";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
                break;
            case 2:
                UpdateTextProduction(productionSpeedLevel3);
                btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceProductionSpeedLevel3}"+" $";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
                break;
            case 3:
                btnProductionSpeed.GetComponent<Image>().color = finished;
                btnProductionSpeed.interactable = false;
                btnProductionSpeed.GetComponentInChildren<TMP_Text>().text = "Fertig";
                productionText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
                break;
        }

        switch(currentRefillSpeedLevel){
            case 0:
                UpdateTextRefill(refillSpeedLevel1);
                btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceRefillSpeedLevel1}"+" $";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "0";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl0;
                break;
            case 1:
                UpdateTextRefill(refillSpeedLevel2);
                btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceRefillSpeedLevel2}"+" $";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "1";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl1;
                break;
            case 2:
                UpdateTextRefill(refillSpeedLevel3);
                btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceRefillSpeedLevel3}"+" $";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "*";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = lvl2;
                break;
            case 3:
                btnRefillSpeed.GetComponent<Image>().color = finished;
                btnRefillSpeed.interactable = false;
                btnRefillSpeed.GetComponentInChildren<TMP_Text>().text = "Fertig";
                refillText.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
                break;
        }

    }
    
}
