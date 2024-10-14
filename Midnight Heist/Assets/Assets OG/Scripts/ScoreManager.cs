using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int currentScore = 0;
    
    public TMP_Text textbox;
    
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return currentScore;
    }
    
    private void Update() {
        textbox.text = GetScore() + "G";
    }

}
