using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject deathMenu;

    private void Update()
    {
        if (player.GetIsDead())
            Show();
        else
            Hide();
    }

    public void RequestBackToMainMenu()
    {
        RestartManager.BackToMainMenu();
    }


    void Show()
    {
        deathMenu.SetActive(true);
    }

    void Hide()
    {
        deathMenu.SetActive(false);
    }
}
