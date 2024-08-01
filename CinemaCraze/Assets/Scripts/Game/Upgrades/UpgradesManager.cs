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
    public AudioHandler audioHandler;
    
    public Upgrades PopcornUpgrades;
    public Upgrades NachosUpgrades;
    public Upgrades SodaUpgrades;
    
    const int popcornCapacityPrice_1 = 10;
    const int popcornCapacity_1 = 10;
    const int popcornCapacity_2 = 20;
    const int popcornCapacity_3 = 30;
    const int popcornProductionTimePrice_1 = 50;
    const float popcornProductionTime_1 = 0.5f;
    const float popcornProductionTime_2 = 0.66f;
    const float popcornProductionTime_3 = 2.0f;
    const int popcornRefillTimePrice_1 = 50;
    const float popcornRefillTime_1 = 0.5f;
    const float popcornRefillTime_2 = 0.66f;
    const float popcornRefillTime_3 = 2.0f;
    const int nachosCapacityPrice_1 = 10;
    const int nachosCapacity_1 = 10;
    const int nachosCapacity_2 = 20;
    const int nachosCapacity_3 = 30;
    const int nachosProductionTimePrice_1 = 50;
    const float nachosProductionTime_1 = 0.5f;
    const float nachosProductionTime_2 = 0.66f;
    const float nachosProductionTime_3 = 2.0f;
    const int nachosRefillTimePrice_1 = 50;
    const float nachosRefillTime_1 = 0.5f;
    const float nachosRefillTime_2 = 0.66f;
    const float nachosRefillTime_3 = 2.0f;
    const int sodaCapacityPrice_1 = 10;
    const int sodaCapacity_1 = 10;
    const int sodaCapacity_2 = 20;
    const int sodaCapacity_3 = 30;
    const int sodaProductionTimePrice_1 = 50;
    const float sodaProductionTime_1 = 0.5f;
    const float sodaProductionTime_2 = 0.66f;
    const float sodaProductionTime_3 = 2.0f;
    const int sodaRefillTimePrice_1 = 50;
    const float sodaRefillTime_1 = 0.5f;
    const float sodaRefillTime_2 = 0.66f;
    const float sodaRefillTime_3 = 2.0f;
    void Start()
    {
        btn_upgrades.onClick.AddListener(OpenUpgradesMenu);
        btn_close.onClick.AddListener(CloseUpgradesMenu);
    }
   
    public void OpenUpgradesMenu()
    {
        audioHandler.PlayClick();
        upgradesMenu.SetActive(true);
        btn_upgrades.gameObject.SetActive(false);
    }
    public void CloseUpgradesMenu()
    {
        audioHandler.PlayClick();
        upgradesMenu.SetActive(false);
        btn_upgrades.gameObject.SetActive(true);
    }
    public void ResetUpgrades(){
        
        PopcornUpgrades.ResetUpgrades();

        NachosUpgrades.ResetUpgrades();

        SodaUpgrades.ResetUpgrades();
        
  
        
    }
    public void LoadUpgrades(GameData gameData){
        PopcornUpgrades.LoadUpgrades(gameData,0);
        NachosUpgrades.LoadUpgrades(gameData,1);
        SodaUpgrades.LoadUpgrades(gameData,2);
    }
}
