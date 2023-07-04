using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    public MenuManager menuManager;
    public CameraSwitch cameraSwitch;
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

        // * Toggles the CameraSwitcher
        else if(Input.GetKeyDown(KeyCode.A))
        {
            cameraSwitch.Toggle();
        }
    }

}
