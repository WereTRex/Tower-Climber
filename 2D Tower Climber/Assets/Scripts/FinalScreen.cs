using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText;

    private void Awake()
    {
        displayText.text = "Score: " + ScoreManager.GetCurrentScore().ToString("0000");

        //Unload all scenes other than the manager/this scene
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!(SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexes.MANAGER || SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexes.FINAL_SCREEN))
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
                Debug.Log(SceneManager.GetSceneAt(i).buildIndex);
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManagerScript.LoadMenu();
    }
}
