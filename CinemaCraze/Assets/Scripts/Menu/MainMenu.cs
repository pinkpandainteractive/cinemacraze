using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// MainMenu-Behavior
/// </summary>
public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// Loads the GameScene
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Loading Game");
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Stops the Application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
