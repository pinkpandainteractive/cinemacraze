using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    public MenuManager menuManager;
    public TimeManager timeManager;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.pauseMenu.Toggle();
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            timeManager.Toggle();
        }
    }

}
