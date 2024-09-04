using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Pause_Menu : MonoBehaviour
{
    public static Pause_Menu instance;
    public GameObject gameOverText;
    public GameObject ResumeMenu;
    private Volume postProcessVolume;
    private DepthOfField depthOfField;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);

        postProcessVolume = FindObjectOfType<Volume>();
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGet(out depthOfField);
        }
        else
        {
            Debug.LogWarning("No post-process volume found in the scene!");
        }
    }
    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
        depthOfField.mode.value = DepthOfFieldMode.Bokeh;
        ResumeMenu.SetActive(true);
        if(Player.isplayerDied == true)
        {
            gameOverText.SetActive(true);
            ResumeMenu.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void HidePauseMenu()
    {
        gameObject.SetActive(false);
        if (gameOverText.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        depthOfField.mode.value = DepthOfFieldMode.Gaussian;
    }
}
