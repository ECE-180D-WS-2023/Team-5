using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=AcpaYq0ihaM for code on keeping score

public class GameManager : MonoBehaviour
{

    public Ball ball;

    public Text playerScoreText;

    public Text computerScoreText;

    private int _playerScore;

    private int _computerScore;

    // keeping track of player score and resetting ball after score
    public void PlayerScores()
    {
        _playerScore++;

        this.playerScoreText.text = _playerScore.ToString();
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
    }

    // keeping track of computer score and resetting ball after score
    public void ComputerScores()
    {
        _computerScore++;

        this.computerScoreText.text = _computerScore.ToString();
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
    }

    void Update()
    {
        //press P to pause
        if(Input.GetKey(KeyCode.P))
        {
            if(Time.timeScale == 1.0f)
            {
                Time.timeScale = 0f;
            }
        }
        //press R to resume
        else if(Input.GetKey(KeyCode.R))
        {
            if(Time.timeScale == 0f)
            {
                Time.timeScale = 1.0f;
            }
        }
    }

}
