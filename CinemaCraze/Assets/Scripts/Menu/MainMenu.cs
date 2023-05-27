using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuGO;
    public MenuManager menuManager;
    public TimeManager timeManager;
  
    public void Start() 
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        menuManager.HideMainMenu();
        menuManager.HidePauseMenu();
        menuManager.EnablePauseMenu();
        menuManager.ShowGameOverlay();
        timeManager.Resume();
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
