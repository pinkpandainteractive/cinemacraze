using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuGO;
    public MenuManager menuManager;
    public TimeManager timeManager;
    public CustomerManager customerManager;
    public Score score;
    public Lives lives;
    public Inventory inventory;
    public bool paused = false;
    public bool Enabled = true;


    public void Start() {
        Debug.Log("Start PauseMenu");
    }
    public void Toggle()
    {
        Debug.Log("Toggle PauseMenu");
        if(Enabled)
        {
            if(paused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resumed Game");
        Hide();
        menuManager.ShowGameOverlay();
        timeManager.Resume();
        paused = false;
    }

    public void Pause()
    {
        Debug.Log("Paused Game");
        Show();
        menuManager.HideGameOverlay();
        timeManager.Pause();
        paused = true;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        Hide();
        Disable();
        customerManager.Reset();
        customerManager.gameRunning = false;
        inventory.Clear();
        score.ResetScore();
        lives.ResetLives();
        menuManager.HideGameOverlay();
        menuManager.ShowMainMenu();
        timeManager.Pause();
        paused = false;
    }

    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }

    public void Show()
    {
        pauseMenuGO.SetActive(true);
    }

    public void Hide()
    {
        pauseMenuGO.SetActive(false);
    }
    
}
