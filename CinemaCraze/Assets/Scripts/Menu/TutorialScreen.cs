using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI tutorialGameStartText;
    public Button nextTextButton;

    public AudioSource source; 
    public AudioClip buttonsound;
    public void Show()
    {
        source.PlayOneShot(buttonsound,1f);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        source.PlayOneShot(buttonsound,1f);
        gameObject.SetActive(false);
    }
    public void ChangeText()
    {
        source.PlayOneShot(buttonsound,1f);
        if(tutorialText != null && tutorialText.gameObject.activeSelf) {
            tutorialText.gameObject.SetActive(false);
            tutorialGameStartText.gameObject.SetActive(true);
            nextTextButton.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            tutorialText.gameObject.SetActive(true);
            tutorialGameStartText.gameObject.SetActive(false);
            nextTextButton.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}
