using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScreen : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI tutorialGameStartText;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ChangeText()
    {
        
        if(tutorialText != null && tutorialText.gameObject.activeSelf) {
            tutorialText.gameObject.SetActive(false);
            tutorialGameStartText.gameObject.SetActive(true);
        }
        else
        {
            tutorialText.gameObject.SetActive(true);
            tutorialGameStartText.gameObject.SetActive(false);
        }
        
    }
}
