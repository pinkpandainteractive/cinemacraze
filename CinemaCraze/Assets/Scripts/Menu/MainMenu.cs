
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;

    void Start()
    {
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
