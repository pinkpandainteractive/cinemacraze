using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int scoreToAdd)
    {
        Debug.Log("Adding " + scoreToAdd + " to score");
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        Debug.Log("Resetting score");
        score = 0;
        scoreText.text = score.ToString();
    }

    public void SubtractScore(int scoreToSubtract)
    {
        Debug.Log("Subtracting " + scoreToSubtract + " from score");
        score -= scoreToSubtract;

        if(score < 0)
        {
            score = 0;
        }

        scoreText.text = score.ToString();
    }

    public void SetScore(int scoreToSet)
    {
        Debug.Log("Setting score to " + scoreToSet);
        score = scoreToSet;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public string GetScoreString()
    {
        return "Score: " + score;
    }

}