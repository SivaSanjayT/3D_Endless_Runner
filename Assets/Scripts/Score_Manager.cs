
using TMPro;
using UnityEngine;


public class Score_Manager : MonoBehaviour
{
    public static Score_Manager instance;

    public TMP_Text scoreText;
    public TMP_Text HighScoretext;
    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadHighScore();
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
        CheckForHighScore();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        LoadHighScore();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score:" + score.ToString();
        HighScoretext.text = "HighScore:" + highScore.ToString();
    }

    private void CheckForHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
