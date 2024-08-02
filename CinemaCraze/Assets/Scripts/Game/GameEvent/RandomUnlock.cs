using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class RandomUnlock : MonoBehaviour
{
    public MachineManager machineManager;
    public Canvas warningScreen;
    private bool hasUnlockedMachine = false;

    private const float TIME_TO_START_EVENT = 180;
    private const float TIME_TO_TRIGGER_NEW_ORDER = 60;
    void Start()
    {
        StartCoroutine(StartEventCoroutine());
    }
    
    public void Reset(){
        StopCoroutine(StartEventCoroutine());
        StopCoroutine(ShowWarningAndUnlock());
        hasUnlockedMachine = false;
        machineManager.isFirstBuy = false;
        machineManager.isSecondBuy = false;
    }
    public IEnumerator StartEventCoroutine()
    {
        
        yield return new WaitUntil(() => 
            !hasUnlockedMachine &&
            Time.time > (TIME_TO_START_EVENT + machineManager.timeFirstBuy) &&
            machineManager.isFirstBuy
        );
        if(machineManager.isSecondBuy){
            yield return new WaitUntil(() => 
                !hasUnlockedMachine &&
                Time.time > (TIME_TO_START_EVENT + machineManager.timeFirstBuy + machineManager.timeSecondBuy)
            );
        }
        Debug.Log("Start Event Coroutine");
        // Event auslösen
        StartCoroutine(ShowWarningAndUnlock());
        hasUnlockedMachine = true;
    }
    public IEnumerator ShowWarningAndUnlock()
    {
        // Maschine auswählen, die freigeschaltet wird
        int machineToUnlock = SelectMachine();

     
        if (machineToUnlock == -1)
        {
            yield break;
        }

        // Warntext basierend auf der ausgewählten Maschine einstellen
        TMP_Text textComponent = warningScreen.transform.GetChild(2).GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            switch (machineToUnlock)
            {
                case -1:
                    StopCoroutine(StartEventCoroutine());
                    StopCoroutine(ShowWarningAndUnlock());
                    break;
                case 0:
                    textComponent.text = "Popcorn";
                    break;
                case 1:
                    textComponent.text = "Nachos";
                    break;
                case 2:
                    textComponent.text = "Soda";
                    break;
            }
        }

        // Zeige Warnbildschirm an
        ShowWarningScreen();
        yield return new WaitForSeconds(5);
        HideWarningScreen();
        // Warte 60 Sekunden
        yield return new WaitForSeconds(TIME_TO_TRIGGER_NEW_ORDER);

        // Schalte neues Produkt frei
        UnlockMachine(machineToUnlock);
        StopCoroutine(StartEventCoroutine());
        
    }

   private int SelectMachine()
{
   
    List<int> machines = new List<int> { 0, 1, 2 };

    
    if (machineManager.popcornMachineUnlocked)
        machines.Remove(0);
    if (machineManager.nachosMachineUnlocked)
        machines.Remove(1);
    if (machineManager.sodaMachineUnlocked)
        machines.Remove(2);

  
    if (machines.Count == 0)
        return -1;

   
    int randomIndex = Random.Range(0, machines.Count);
    return machines[randomIndex];
}

    public void ShowWarningScreen()
    {
        warningScreen.gameObject.SetActive(true);
    }
    public void HideWarningScreen()
    {
        warningScreen.gameObject.SetActive(false);
    }

    public void UnlockMachine(int machineToUnlock)
    {
        Debug.Log("Game Time: " + Time.time);
        Debug.Log("Real Time: " + Time.realtimeSinceStartup);

        switch (machineToUnlock)
        {
            case 0:
                machineManager.popcornMachineUnlocked = true;
                Debug.Log("Popcorn Machine Unlocked");
                break;
            case 1:
                machineManager.nachosMachineUnlocked = true;
                Debug.Log("Nachos Machine Unlocked");
                break;
            case 2:
                machineManager.sodaMachineUnlocked = true;
                Debug.Log("Soda Machine Unlocked");
                break;
        }
    }
}
