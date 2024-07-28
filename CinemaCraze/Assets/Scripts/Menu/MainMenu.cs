using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonsound;
    public AudioSource source; 
    public CustomerManager customerManager;
    public GameObject mainMenuGO;
    public MenuManager menuManager;
    public Score score;
    public Lives lives;
    public Inventory inventory;
    public SaveLoadAgent saveLoadAgent;
    public MachineManager machineManager;

    public void Play()
    {
        source.PlayOneShot(buttonsound,1f);
        
        Debug.Log("Play");
        score.ResetScore();
        lives.ResetLives();
        inventory.Clear();

        menuManager.HideMainMenu();
        menuManager.HidePauseMenu();
        menuManager.EnablePauseMenu();
        menuManager.ShowGameOverlay();
        menuManager.ShowTutorialScreen();
        Time.timeScale = 1f;
        customerManager.status = LiveCycleStatus.Active;
        customerManager.isGameRunning = true;
        machineManager.Reset();
    }

    public void Load()
    {
        source.PlayOneShot(buttonsound,1f);

        machineManager.Reset();
        saveLoadAgent.Load();
        menuManager.HideMainMenu();
        menuManager.HidePauseMenu();
        menuManager.EnablePauseMenu();
        menuManager.ShowGameOverlay();
        Time.timeScale = 1f;
        customerManager.status = LiveCycleStatus.Active;
        customerManager.isGameRunning = true;
    }

    public void Show()
    {
        mainMenuGO.SetActive(true);
    }

    public void Hide()
    {
        mainMenuGO.SetActive(false);
    }

}
