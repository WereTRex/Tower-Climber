using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public delegate void UpdateScoreUI();
    public static event UpdateScoreUI OnUpdateScoreUI;

    [SerializeField] static int currentScore;
    [SerializeField] TextMeshProUGUI scoreText;

    #region Event Stuff
    private void OnEnable()
    {
        OnUpdateScoreUI += UpdateUI;
    }
    private void OnDisable()
    {
        OnUpdateScoreUI -= UpdateUI;
    }
    #endregion

    private void Awake()
    {
        currentScore = 0;
    }


    public static void AddScore(int scoreIncrease)
    {
        currentScore += scoreIncrease;

        if (currentScore >= 10000)
            currentScore = 9999;

        OnUpdateScoreUI?.Invoke();
    }

    public static int GetCurrentScore()
    {
        return currentScore;
    }

    public void UpdateUI()
    {
        //The '"" + currentScore.ToString("0000")' allows the score to look like this: 'Score: 0920'
        scoreText.text = "" + currentScore.ToString("0000");
    }
}
