using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuGO;
    public MenuManager menuManager;
    public TimeManager timeManager;
    public bool paused = false;
    public bool Enabled = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
