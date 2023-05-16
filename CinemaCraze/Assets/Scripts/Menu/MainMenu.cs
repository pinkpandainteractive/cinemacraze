using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;

    public void Start()
    {
        mainMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
  
    public void PlayGame()
    {
        Debug.Log("Loading Game");
        Time.timeScale = 1f;
        mainMenuUI.SetActive(false);   
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
