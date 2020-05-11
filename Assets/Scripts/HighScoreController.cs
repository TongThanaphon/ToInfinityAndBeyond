using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HighScoreController : MonoBehaviour
{

    public AudioMixer mixer;
    public AudioSource bgMusic;
    public Text highScoreValue;

    void Awake() {
        
    }

    void Start() {
        mixer.SetFloat("SFX_VOL", PlayerPrefs.GetFloat("SFX_VOL"));
        mixer.SetFloat("BG_VOL", PlayerPrefs.GetFloat("BG_VOL"));
    }

    void Update() {
        if (PlayerPrefs.HasKey("HighScoreValue")) {
            highScoreValue.text = PlayerPrefs.GetInt("HighScoreValue").ToString();
        }
    }

    public void ResetHighScore() {
        PlayerPrefs.SetInt("HighScoreValue", 0);
    }
}
