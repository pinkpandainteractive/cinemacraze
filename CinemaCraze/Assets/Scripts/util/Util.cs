using UnityEngine;

public class Util : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quitting Game");

        // * Stop playing in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;

        // * Stop playing in built application
        Application.Quit();
    }
}
