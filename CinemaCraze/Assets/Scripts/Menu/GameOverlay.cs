using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    public GameObject overlay;

    public void ShowOverlay()
    {
        Debug.Log("Show Overlay");
        overlay.SetActive(true);
    }

    public void HideOverlay()
    {
        Debug.Log("Hide Overlay");
        overlay.SetActive(false);
    }
}
