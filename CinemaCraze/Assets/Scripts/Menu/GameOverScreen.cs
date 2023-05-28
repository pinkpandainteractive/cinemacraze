using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreenGO;
    public MenuManager menuManager;
    public TimeManager timeManager;
    public Score score;
    public Lives lives;

    public void Show()
    {
        gameOverScreenGO.SetActive(true);
    }

    public void Hide()
    {
        gameOverScreenGO.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restarting Game");
        score.ResetScore();
        lives.ResetLives();
        Hide();
        menuManager.ShowGameOverlay();
        menuManager.HidePauseMenu();
        menuManager.EnablePauseMenu();
        timeManager.Resume();
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        Hide();
        menuManager.HideGameOverlay();
        menuManager.HidePauseMenu();
        menuManager.DisablePauseMenu();
        menuManager.ShowMainMenu();
        timeManager.Pause();
    }
}