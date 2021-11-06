using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Load the next scene
        SceneManagerScript.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit (Doesn't work in inspector");
    }
}
