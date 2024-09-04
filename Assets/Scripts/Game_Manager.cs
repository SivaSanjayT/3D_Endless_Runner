using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public GameObject optionsMenu;
    private bool isGamePasued = false;
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

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Player.isplayerDied = false;
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetScore();
        ResetPlayer();
        Time.timeScale = 1.0f;
    }

    public void ResetPlayer()
    {
        Player.instance.ResetPlayerPos();
        Player.isplayerDied = false;
        Time.timeScale = 1.0f;    
    }
    
    public void ResetScore()
    {
        Score_Manager.instance.ResetScore();
    }
    
    
    // PASUE FUNCTIONS 
    public void PauseMenu()
    {
        isGamePasued = !isGamePasued;

        if (isGamePasued)
        {
            GamePause();
        }
        else
        {
            ResumeGame();
        }
    }

    public void OptionsMenu()
    {
       optionsMenu.SetActive(true);
    }

    public void ExitOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    void GamePause()
    {
        Player.canInput = false;
        Time.timeScale = 0f;
        Pause_Menu.instance.ShowPauseMenu();
    }

    void ResumeGame()
    {
        Player.canInput = true;
        Time.timeScale = 1f;
        Pause_Menu.instance.HidePauseMenu();
    }

}
