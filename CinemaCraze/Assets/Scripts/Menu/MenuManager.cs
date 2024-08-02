using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public AudioClip buttonsound;
    public AudioSource source; 
    public MainMenu mainMenu;
    public PauseMenu pauseMenu;
    public GameOverlay gameOverlay;
    public GameOverScreen gameOverScreen;
    public TutorialScreen tutorialScreen;

    public void Play()
    {
        mainMenu.Play();
    }
    public void ShowMainMenu()
    {
        Debug.Log("Showing Main Menu");
        Time.timeScale = 0f;
        mainMenu.Show();
    }

    public void HideMainMenu()
    {
        Debug.Log("Hiding Main Menu");
        Time.timeScale = 1f;
        mainMenu.Hide();
    }

    public void ShowPauseMenu()
    {
        Debug.Log("Showing Pause Menu");
        pauseMenu.Show();
    }

    public void HidePauseMenu()
    {
        Debug.Log("Hiding Pause Menu");
        pauseMenu.Hide();
    }

    public void ShowGameOverlay()
    {
        Debug.Log("Showing Game Overlay");
        gameOverlay.Show();
    }

    public void HideGameOverlay()
    {
        Debug.Log("Hiding Game Overlay");
        gameOverlay.Hide();
    }

    public void ShowGameOverScreen()
    {
        Debug.Log("Showing Game Over Screen");
        gameOverScreen.Show();
    }

    public void HideGameOverScreen()
    {
        Debug.Log("Hiding Game Over Screen");
        gameOverScreen.Hide();
    }
    public void ShowTutorialScreen()
    {
       tutorialScreen.Show();
    }
    public void HideTutorialScreen()
    {
        tutorialScreen.Hide();
    }
    public void EnablePauseMenu()
    {
        Debug.Log("Enabling Pause Menu");
        pauseMenu.Enable();
    }

    public void DisablePauseMenu()
    {
        Debug.Log("Disabling Pause Menu");
        pauseMenu.Disable();
    }

    public void Quit()
    {
        source.PlayOneShot(buttonsound,1f);

        Debug.Log("Quitting Game");

        // * Stop playing in Unity Editor
        // ! needs to be commented out for built application XXX
        // UnityEditor.EditorApplication.isPlaying = false;

        // * Stop playing in built application
        Application.Quit();
    }

}