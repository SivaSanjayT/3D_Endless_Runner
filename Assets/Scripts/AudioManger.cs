using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class AudioManger : MonoBehaviour
{
    [Header("Music Properties")]
    public Slider MusicSlider;
    public AudioSource MusicSource;
    private void Start()
    {
        if (MusicSource != null)
        {
            MusicSlider.value = MusicSource.volume;
        }
        MusicSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        if (MusicSource != null)
        {
            MusicSource.volume = value;
        }
    }
}
