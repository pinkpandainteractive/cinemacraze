using UnityEngine;

public class Lives : MonoBehaviour
{
    private int lives = 3;
    public GameObject heart3;
    public GameObject heart2;
    public GameObject heart1;
    public GameOverScreen gameOverScreen;
    public GameOverlay gameOverlay;

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
            gameOverScreen.ShowGameOverScreen();
            gameOverlay.HideOverlay();
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

}
