using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameOverlayUI;
    public GameObject mainMenuUI;

    public void ShowGameOverScreen()
    {
        Debug.Log("Show Game Over Screen");
        gameOverScreen.SetActive(true);
    }

    public void HideGameOverScreen()
    {
        Debug.Log("Hide Game Over Screen");
        gameOverScreen.SetActive(false);
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        gameOverScreen.SetActive(false);
        gameOverlayUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        gameOverScreen.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}