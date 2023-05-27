using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    public MenuManager menuManager;
    public CameraSwitch cameraSwitch;
    public TimeManager timeManager;

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
            timeManager.Toggle();
        }

        // * Toggles the CameraSwitcher
        else if(Input.GetKeyDown(KeyCode.A))
        {
            cameraSwitch.Toggle();
        }
    }

}
