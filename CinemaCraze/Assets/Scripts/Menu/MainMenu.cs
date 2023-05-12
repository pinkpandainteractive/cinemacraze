
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
  
    public void PlayGame()
    {
        Debug.Log("Loading Game");     
        mainMenuUI.SetActive(false);   
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
