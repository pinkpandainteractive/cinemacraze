using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public AudioClip buttonsound;
    public AudioSource source; 
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
        source.PlayOneShot(buttonsound,1f);

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
        source.PlayOneShot(buttonsound,1f);
        
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