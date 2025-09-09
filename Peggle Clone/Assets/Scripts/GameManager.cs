using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int ballsRemaining;
    public int pegsLeft;
    public TextMeshProUGUI scoreTextDisplay;
    public TextMeshProUGUI ballsRemaningTextDisplay;
    public GameObject gameOverText;
    public GameObject youWinText;
    private bool resultShown;
    private EntityManager manager;
    void Start()
    {
        ballsRemaining = 10;
        pegsLeft = 20;
        resultShown = false;
        ballsRemaningTextDisplay.text = "Balls: " + ballsRemaining;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !resultShown && ballsRemaining >= 1)
        {
            var e = manager.CreateEntity();
            manager.AddComponent<LaunchBallEvent>(e);
            AdjustBallDisplay();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreTextDisplay.text = "Score: " + score +"/2000";
    }

    public void AdjustBallDisplay()
    {
        ballsRemaining--;
        ballsRemaningTextDisplay.text = "Balls: " + ballsRemaining;
    }

    public void BallFell()
    {
        if (ballsRemaining <= 0)
        {
            EndGame(false);
        }
    }

    public void PegHit()
    {
        AddScore(100);
        pegsLeft--;
        if (pegsLeft <= 0)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool victory)
    {
        if (!resultShown) 
        {
            if (victory)
            {
                youWinText.SetActive(true);
            }
            else
            {
                gameOverText.SetActive(true);
            }
            resultShown = true;
        }
    }
}