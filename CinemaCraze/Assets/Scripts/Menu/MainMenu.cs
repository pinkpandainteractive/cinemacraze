using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameOverlayUI;

    public void Start()
    {
        Time.timeScale = 0f;
    }
  
    public void PlayGame()
    {
        Debug.Log("Loading Game");
        Time.timeScale = 1f;
        gameOverlayUI.SetActive(true);
        mainMenuUI.SetActive(false);   
    }

}
