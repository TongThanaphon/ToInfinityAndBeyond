using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HomeController : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgMusic;
    
    public GameObject howToPlayCanvas;
    public GameObject homeCanvas;

    void Start() {
        mixer.SetFloat("SFX_VOL", PlayerPrefs.GetFloat("SFX_VOL"));
        mixer.SetFloat("BG_VOL", PlayerPrefs.GetFloat("BG_VOL"));
    }

    void Update() {
        
    }

    public void OpenHowToPlay() {
        homeCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }

    public void CloseHowToPlay() {
        howToPlayCanvas.SetActive(false);
        homeCanvas.SetActive(true);
    }
}
