using UnityEngine;

public class Lives : MonoBehaviour
{
    public AudioClip heartLoseSound;
    public AudioSource source; 
    public int lives = 3;
    public GameObject heart3;
    public GameObject heart2;
    public GameObject heart1;
    
    public MenuManager menuManager;

    public void AddLife()
    {
        Debug.Log("Adding life");
        lives++;
        if (lives == 3)
        {
            heart3.SetActive(true);
        }
        else if (lives == 2)
        {
            heart2.SetActive(true);
        }
        else if (lives == 1)
        {
            heart1.SetActive(true);
        }
    }


    public void LoseLife()
    {
        source.PlayOneShot(heartLoseSound,1f);

        Debug.Log("Losing life");
        lives--;
        if (lives == 2)
        {
            heart3.SetActive(false);
        }
        else if (lives == 1)
        {
            heart2.SetActive(false);
        }
        else if (lives == 0)
        {
            heart1.SetActive(false);
            Debug.Log("Game Over");
            menuManager.ShowGameOverScreen();
            menuManager.HideGameOverlay();
            menuManager.DisablePauseMenu();
            Time.timeScale = 0f;
        }
    }

    public void ResetLives()
    {
        Debug.Log("Resetting lives");
        lives = 3;
        heart3.SetActive(true);
        heart2.SetActive(true);
        heart1.SetActive(true);
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        if (lives == 3)
        {
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if (lives == 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if (lives == 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(true);
        }
        // ! should not be the case
        else if (lives == 0)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);
            Debug.LogError("Lives should not be 0 on load");
        }
    }

}
