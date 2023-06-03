using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// https://www.youtube.com/watch?v=AcpaYq0ihaM for code on keeping score

public class GameManager : MonoBehaviour
{
    public Ball ball;

    public Text playerScoreText;

    public Text computerScoreText;

    public Text user1_game_result;

    public Text user2_game_result;

    private int _playerScore;

    private int _computerScore;

    //[SerializeField] private AudioSource refreeWhistle;
    public AudioSource refreeWhistle;
	
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
        Scene scene = SceneManager.GetActiveScene();
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

        // ====== CHRIS =====

        //Ending the game after the certain scores
        if (scene.name == "Tutorial")
        { }
        else
        {
            if (_computerScore == 5 || _playerScore == 5)
            {
                this.ball.ResetPosition();
                refreeWhistle.Play();
            }
        }

    }

}
