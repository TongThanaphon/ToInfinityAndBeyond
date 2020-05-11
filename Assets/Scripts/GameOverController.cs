using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    
    public Text totalScoreValue;
    public Text scoreValue;
    public Text bonusValue;

    private int totalScore;
    private int score;
    private int bonus;
    private int highScore;

    void Awake() {
        if (PlayerPrefs.HasKey("ScoreValue")) {
            score = PlayerPrefs.GetInt("ScoreValue");
            scoreValue.text = score.ToString();
        }

        if (PlayerPrefs.HasKey("BonusValue")) {
            bonus = PlayerPrefs.GetInt("BonusValue");
            bonusValue.text = "x " + bonus.ToString();
        }

        totalScore = score * bonus;
        totalScoreValue.text = totalScore.ToString();

        if (PlayerPrefs.HasKey("HighScoreValue")) {
            highScore = PlayerPrefs.GetInt("HighScoreValue");

            if (totalScore > highScore) {
                PlayerPrefs.SetInt("HighScoreValue", totalScore);
            }
        }
    }

    void Start() {
        PlayerPrefs.DeleteKey("ScoreValue");
        PlayerPrefs.DeleteKey("BonusValue");
        PlayerPrefs.DeleteKey("IsLastChance");
    }

    void Update() {

    }
}
