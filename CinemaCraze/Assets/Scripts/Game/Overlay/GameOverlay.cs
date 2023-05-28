using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    public GameObject gameOverlayGO;

    public void Show()
    {
        gameOverlayGO.SetActive(true);
    }

    public void Hide()
    {
        gameOverlayGO.SetActive(false);
    }
}
