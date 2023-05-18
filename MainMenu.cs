using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 0 : Main Menu
// 1 : Single Player Mode
// 2 : Explanation (Intro)
// 3 : Tutorial
// 4 : Multiplayer Mode
public class MainMenu : MonoBehaviour
{
    public void ReturnMainFromIntro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); //Intro -> Main Menu
    }
    public void ReturnMainFromTuto()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3); //Main Menu -> Intro
    }
    public void SinglePlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Main menu -> Single
    }
    public void Intro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //Main Menu -> Intro
    }
    public void MultiPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4); //Main Menu -> Multi
    }
    public void IntroToTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Intro -> Tutorial
    }
  
}
