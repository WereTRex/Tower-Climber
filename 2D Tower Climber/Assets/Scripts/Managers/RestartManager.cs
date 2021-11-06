using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public void OnRestartTriggered(InputAction.CallbackContext context)
    {
        RestartScene();
    }

    public static void RestartScene()
    {
        //Restart The Scene (Note: If there are multiple scenes loaded at the same time, instead fire off an event and trigger the appropriate functions for each gameobject). Otherwise:
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex >= (int)SceneIndexes.LEVEL_1 && SceneManager.GetSceneAt(i).buildIndex <= (int)SceneIndexes.LEVEL_4)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
                SceneManager.LoadScene(SceneManager.GetSceneAt(i).buildIndex, LoadSceneMode.Additive);
            }
        }
    }

    public static void BackToMainMenu()
    {
        //Load the Menu Scene, which should be scene 0
        SceneManager.LoadScene(0);
    }
}
