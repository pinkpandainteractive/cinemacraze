using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradesManager : MonoBehaviour
{
    public Button btn_upgrades;
    public Button btn_close;
    public GameObject upgradesMenu;
    public Score score;
    public GameObject popcorn;
    public GameObject nachos;
    public GameObject soda;
    private Color basic = Color.white;
    private Color invalid = Color.red;
    private Color finished = new(1.0f, 0.31f, 0.58f, 1.0f);
    [Header("Popcorn Upgrades")]
    public Button btn_popcornRefillSpeed;
    public Button btn_popcornProductionSpeed;
    public Button btn_popcornCapacity;
    public TMP_Text popcornCapacityText;
    public int popcornCapacityLevel = 20;
    public int pricePopcornCapacityLevel = 10000;
    public float popcornProductionSpeed = 2.0f;
    public int pricePopcornProductionSpeed = 25000;
    public float popcornRefillSpeed = 2.0f;
    public int pricePopcornRefillSpeed = 25000;
    [Header("Nacho Upgrades")]
    public Button btn_nachoRefillSpeed;
    public Button btn_nachoProductionSpeed;
    public Button btn_nachoCapacity;
    public TMP_Text nachoCapacityText;
    public int nachoCapacityLevel = 20;
    public int priceNachoCapacityLevel = 10000;
    public float nachoProductionSpeed = 2.0f;
    public int priceNachoProductionSpeed = 25000;
    public float nachoRefillSpeed = 2.0f;
    public int priceNachoRefillSpeed = 25000;
    [Header("Soda Upgrades")]
    public Button btn_sodaRefillSpeed;
    public Button btn_sodaProductionSpeed;
    public Button btn_sodaCapacity;
    public TMP_Text sodaCapacityText;
    public int sodaCapacityLevel = 20;
    public int priceSodaCapacityLevel = 10000;
    public float sodaProductionSpeed = 2.0f;
    public int priceSodaProductionSpeed = 25000;
    public float sodaRefillSpeed = 2.0f;
    public int priceSodaRefillSpeed = 25000;

    // Start is called before the first frame update
    void Start()
    {
        btn_upgrades.onClick.AddListener(OpenUpgradesMenu);
        btn_close.onClick.AddListener(CloseUpgradesMenu);
        UpdatePreis();
        UpdateText();
        btn_popcornProductionSpeed.onClick.AddListener(delegate {
            UpgradeProduction(popcorn, pricePopcornProductionSpeed, btn_popcornProductionSpeed,popcornProductionSpeed);
        });
        btn_popcornRefillSpeed.onClick.AddListener(delegate{ 
            UpgradeRefill(popcorn,pricePopcornRefillSpeed,btn_popcornRefillSpeed,popcornRefillSpeed); });

        btn_popcornCapacity.onClick.AddListener(delegate {
            UpgradeCapacity(popcorn, pricePopcornCapacityLevel, btn_popcornCapacity,popcornCapacityLevel);
        });
        // * Button nachos
        btn_nachoProductionSpeed.onClick.AddListener(delegate {
            UpgradeProduction(nachos, priceNachoProductionSpeed, btn_nachoProductionSpeed, nachoProductionSpeed);
        });
        btn_nachoRefillSpeed.onClick.AddListener(delegate {
            UpgradeRefill(nachos, priceNachoRefillSpeed, btn_nachoRefillSpeed, nachoRefillSpeed);
        });

        btn_nachoCapacity.onClick.AddListener(delegate {
            UpgradeCapacity(nachos, priceNachoCapacityLevel, btn_nachoCapacity, nachoCapacityLevel);
        });
        // * Button soda
        btn_sodaProductionSpeed.onClick.AddListener(delegate {
            UpgradeProduction(soda, priceSodaProductionSpeed, btn_sodaProductionSpeed, sodaProductionSpeed);
        });
        btn_sodaRefillSpeed.onClick.AddListener(delegate {
            UpgradeRefill(soda, priceSodaRefillSpeed, btn_sodaRefillSpeed, sodaRefillSpeed);
        });
        btn_sodaCapacity.onClick.AddListener(delegate {
            UpgradeCapacity(soda, priceSodaCapacityLevel, btn_sodaCapacity, sodaCapacityLevel);
        });
    }
    public void UpgradeProduction(GameObject obj, int price, Button btn,float speed)
    {
        if (obj.GetComponent<Product>().productionUpgradeStatus.Equals(Product.ProductionUpgradeStatus.Done)) return;
        if (score.GetScore() < price)
        {
            btn.GetComponent<Image>().color = invalid;
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }
        obj.GetComponent<Product>().productionTime /= speed;
        btn.GetComponent<Image>().color = finished;
        btn.interactable = false;
        btn.GetComponentInChildren<TMP_Text>().text = "Gekauft";
        obj.GetComponent<Product>().productionUpgradeStatus = Product.ProductionUpgradeStatus.Done;
    }
    public void UpgradeRefill(GameObject obj,int price,Button btn,float speed)
    {
        if (obj.GetComponent<Product>().refillUpgradeStatus.Equals(Product.RefillUpgradeStatus.Done)) return;
        if (score.GetScore() < price)
        {
            btn.GetComponent<Image>().color = invalid;
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }
        obj.GetComponent<Product>().refillTime /= speed;
        btn.GetComponent<Image>().color = finished;
        btn.interactable = false;
        btn.GetComponentInChildren<TMP_Text>().text = "Gekauft";
        obj.GetComponent<Product>().refillUpgradeStatus = Product.RefillUpgradeStatus.Done;
    }

    public void UpgradeCapacity(GameObject obj, int price, Button btn, int cap)
    {
        if (obj.GetComponent<Product>().refillUpgradeStatus.Equals(Product.Capacity1UpgradeStatus.Done)) return;

        if (score.GetScore() < price)
        {
            btn.GetComponent<Image>().color = invalid;
            StartCoroutine(ChangeButtonColorForSec(btn));
            return;
        }
        obj.GetComponent<Product>().capacity = cap;
        obj.GetComponent<Product>().maxCapacity = cap;
        btn.GetComponent<Image>().color = finished;
        btn.interactable = false;
        btn.GetComponentInChildren<TMP_Text>().text = "Gekauft";
        obj.GetComponent<Product>().capacity1UpgradeStatus = Product.Capacity1UpgradeStatus.Done;
    }
    void UpdatePreis()
    {
        btn_popcornProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{pricePopcornProductionSpeed}";
        btn_popcornRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{pricePopcornRefillSpeed}";
        btn_popcornCapacity.GetComponentInChildren<TMP_Text>().text = $"{pricePopcornCapacityLevel}";

        btn_nachoProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceNachoProductionSpeed}";
        btn_nachoRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceNachoRefillSpeed}";
        btn_nachoCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceNachoCapacityLevel}";

        btn_sodaProductionSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceSodaProductionSpeed}";
        btn_sodaRefillSpeed.GetComponentInChildren<TMP_Text>().text = $"{priceSodaRefillSpeed}";
        btn_sodaCapacity.GetComponentInChildren<TMP_Text>().text = $"{priceSodaCapacityLevel}";
    }
    void UpdateText()
    {
        popcornCapacityText.text = "Kapazität auf "+ popcornCapacityLevel.ToString()+" erhöhen";
        nachoCapacityText.text = "Kapazität auf " + nachoCapacityLevel.ToString() + " erhöhen";
        sodaCapacityText.text = "Kapazität auf " + sodaCapacityLevel.ToString() + " erhöhen";
    }
    public void OpenUpgradesMenu()
    {
        upgradesMenu.SetActive(true);
        btn_upgrades.gameObject.SetActive(false);
    }
    public void CloseUpgradesMenu()
    {
        upgradesMenu.SetActive(false);
        btn_upgrades.gameObject.SetActive(true);
    }
    IEnumerator ChangeButtonColorForSec(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        btn.GetComponent<Image>().color = basic;
    }
}
