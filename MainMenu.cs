using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SinglePlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //goes to next scene index which from main menu is single player
    }
    public void MultiPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //goes to scene two scenes after main menu index
    }
}
