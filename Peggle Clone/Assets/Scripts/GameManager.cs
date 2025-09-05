using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int ballsRemaining;
    public int orangeBricksLeft;
    public TextMeshProUGUI scoreTextDisplay;
    public TextMeshProUGUI ballsRemaningTextDisplay;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWinText;
    private bool resultShown;
    void Start()
    {
        ballsRemaining = 10;
        resultShown = false;
    }

    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreTextDisplay.text = "Score: " + score;
    }

    public void ReduceBall() //change this later
    {
        ballsRemaining--;
        ballsRemaningTextDisplay.text = "Balls: " + ballsRemaining;
        if (ballsRemaining <= 0)
        {
            GameOver(false);
        }
    }

    public void ReduceOrangeBrick()
    {
        orangeBricksLeft--;
        if (orangeBricksLeft <= 0)
        {
            GameOver(true);
        }
    }

    private void GameOver(bool victory)
    {
        if (!resultShown)   //if a result message has already been shown it can't check it again
        {
            if (victory)
            {
                youWinText.enabled = true;
            }
            else
            {
                gameOverText.enabled = true;
            }
            resultShown = true;
        }
    }
}