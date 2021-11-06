using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerScript
{
    public static void StartGame()
    {
        //Unload the menu scene
        SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN);

        //Load the player scene (Additive alows you to load multiple scenes in one enviroment)
        SceneManager.LoadScene((int)SceneIndexes.PLAYERS_AND_CAMERAS, LoadSceneMode.Additive);
        //Load the Score Scene
        SceneManager.LoadScene((int)SceneIndexes.SCORE_SCENE, LoadSceneMode.Additive);
        //Load the first level
        SceneManager.LoadScene((int)SceneIndexes.LEVEL_1, LoadSceneMode.Additive);
    }

    public static void LoadMenu()
    {
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!(SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexes.MANAGER))
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
            }
        }

        SceneManager.LoadScene((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    public static void LoadNextLevel(int levelSceneNumber)
    {
        //Unload the current level
        SceneManager.UnloadSceneAsync(levelSceneNumber);
        SceneManager.LoadSceneAsync(levelSceneNumber + 1, LoadSceneMode.Additive);
    }

    public static void OnEntranceDetailsGotten(GameObject levelEntrance)
    {
        //Reset the player's position at the level entrance

        //  Find the player
        GameObject player = null;
        foreach (GameObject obj in SceneManager.GetSceneByBuildIndex((int)SceneIndexes.PLAYERS_AND_CAMERAS).GetRootGameObjects())
        {
            if (obj.CompareTag("Player"))
            {
                player = obj;
                break;
            }
        }

        //  Set the player's position
        Debug.Log("Player:" + player + ". Level Entrance:" + levelEntrance);
        player.transform.position = new Vector3(levelEntrance.transform.position.x, levelEntrance.transform.position.y, player.transform.position.z);
    }
}
