using UnityEngine;
using TMPro;

public class GameOverlay : MonoBehaviour
{

    public TextMeshPro orderText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
