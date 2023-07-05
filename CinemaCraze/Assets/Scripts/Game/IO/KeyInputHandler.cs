using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    public MenuManager menuManager;
    public Camera mainCamera;
    public Camera productCamera;
    void Update()
    {
        // ! Bitte alle Tastenbelegungen hier eintragen

        // * Toggles Pause Menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.pauseMenu.Toggle();
        }

        // * Toggles if the time is paused
        else if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        // * Switches to main camera
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mainCamera.enabled = true;
            productCamera.enabled = false;
        }

        // * Switches to product camera
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            mainCamera.enabled = false;
            productCamera.enabled = true;
        }
    }

}
