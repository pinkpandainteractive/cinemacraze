using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public AudioClip buttonsound;
    public AudioSource source; 
    public GameObject pauseMenuGO;
    public MenuManager menuManager;
    public CustomerManager customerManager;
    public RandomUnlock gameEvent;
    public Score score;
    public Lives lives;
    public Inventory inventory;
    public bool paused = false;
    public bool Enabled = true;

    private float pausedTime = 0f;
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
        source.PlayOneShot(buttonsound, 1f);

        Debug.Log("Resumed Game");
        Hide();
        menuManager.ShowGameOverlay();
        Time.timeScale = 1f;
        paused = false;
        pausedTime += Time.realtimeSinceStartup;
    }

    public void Pause()
    {
        Debug.Log("Paused Game");
        Show();
        menuManager.HideGameOverlay();
        Time.timeScale = 0f;
        paused = true;
    }

    public void ReturnToMainMenu()
    {
        source.PlayOneShot(buttonsound, 1f);
        
        Debug.Log("Returning to Main Menu");
        Hide();
        Disable();
        customerManager.Reset();
        customerManager.isGameRunning = false;
        inventory.Clear();
        score.ResetScore();
        lives.ResetLives();
        menuManager.HideGameOverlay();
        menuManager.ShowMainMenu();
        Time.timeScale = 0f;
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
    public void DisplayGameTime()
    {
        float gameTime = Time.time;
        float realTime = Time.realtimeSinceStartup - pausedTime; 
        Debug.Log("Game Time: " + gameTime + " seconds");
        Debug.Log("Real Time: " + realTime + " seconds");
    }
}
