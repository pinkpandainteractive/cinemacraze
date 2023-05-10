
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;

    public bool PlayGame()
    {
        Debug.Log("Loading Game");
        if(mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
        
        
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
