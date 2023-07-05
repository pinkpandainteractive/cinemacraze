using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public MenuManager menuManager;
    public CustomerManager customerManager;
    public Score score;
    public Lives lives;
    public Inventory inventory;
    public TMP_Text scoreText;

    public void Show()
    {
        scoreText.text = score.GetScoreString();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restarting Game");
        customerManager.Reset();
        score.ResetScore();
        lives.ResetLives();
        inventory.Clear();
        Hide();
        menuManager.Play();
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        Hide();
        customerManager.Reset();
        customerManager.isGameRunning = false;
        inventory.Clear();
        score.ResetScore();
        lives.ResetLives();

        menuManager.HideGameOverlay();
        menuManager.HidePauseMenu();
        menuManager.DisablePauseMenu();
        menuManager.ShowMainMenu();
        Time.timeScale = 0f;
    }
}