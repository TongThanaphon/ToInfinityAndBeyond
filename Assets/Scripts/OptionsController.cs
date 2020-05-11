using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    public AudioSource bgMusic;

    public AudioMixer mixer;
    public Slider sfxSlider;
    public Slider bgSlider;

    void Start() {
        if (PlayerPrefs.HasKey("SFX_VOL")) {
            sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOL");
        }

        if (PlayerPrefs.HasKey("BG_VOL")) {
            bgSlider.value = PlayerPrefs.GetFloat("BG_VOL");
        }
    }

    void Update() {
        mixer.SetFloat("SFX_VOL", sfxSlider.value);
        mixer.SetFloat("BG_VOL", bgSlider.value);
    }

    public void SaveAduioSettings() {
        PlayerPrefs.SetFloat("SFX_VOL", sfxSlider.value);
        PlayerPrefs.SetFloat("BG_VOL", bgSlider.value);
        PlayerPrefs.Save();
    }
}
