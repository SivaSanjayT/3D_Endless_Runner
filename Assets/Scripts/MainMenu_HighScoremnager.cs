using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu_HighScoremnager : MonoBehaviour
{
    public TMP_Text highScoreText;

    private void Start()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
