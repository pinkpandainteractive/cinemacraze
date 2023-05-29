using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CustomerManager customerManager;
    public GameObject mainMenuGO;
    public MenuManager menuManager;
    public TimeManager timeManager;
    public Score score;
    public Lives lives;
  
    public void Start() 
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        score.ResetScore();
        lives.ResetLives();
        menuManager.HideMainMenu();
        menuManager.HidePauseMenu();
        menuManager.EnablePauseMenu();
        menuManager.ShowGameOverlay();
        timeManager.Resume();
        customerManager.StartLiveCycle();
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
