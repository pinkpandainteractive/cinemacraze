using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject gameOverlayUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
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
        Debug.Log("Resume");
        pauseMenuUI.SetActive(false);
        gameOverlayUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pause");
        pauseMenuUI.SetActive(true);
        gameOverlayUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
