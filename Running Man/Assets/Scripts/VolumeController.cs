using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer soundEffectMixer;
    public Slider slider;
    public Slider slider2;

    public void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        slider2.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetVolume2(float volume2)
    {
        soundEffectMixer.SetFloat("volume", Mathf.Log10(volume2) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume2);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
