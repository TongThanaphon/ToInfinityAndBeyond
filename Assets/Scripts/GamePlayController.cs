using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GamePlayController : MonoBehaviour
{

    private SceneConntroller sc;

    public AudioMixer mixer;
    public AudioSource bgMusic;
    public AudioSource bgMusic2;
    public AudioSource bonusSfx;
    public AudioSource winSfx;
    public AudioSource loseSfx;

    public Text scoreText;
    public Text bonusText;
    public Text resultText;
    public Text bonusCountText;
    public Text titleText;
    public Text nextBtnText;
    public Text prevScoreText;
    public GameObject minigameButtons;

    public GameObject lastChanceCanvas;
    public GameObject player;
    public GameObject floorPrefab;
    public GameObject superFloorPrefab;
    public GameObject bonusTextPrefab;
    public GameObject ghostPrefab;
    public GameObject batPrefab;
    public GameObject slimePrefab;

    public GameObject centerBackground;
    public GameObject upperBackground;
    public GameObject lowerBackground;

    public Material soilMaterial;
    public Material skyMaterial1;
    public Material skyMaterial2;
    public Material galaxyMaterial1;
    public Material galaxyMaterial2;

    private GameObject monsterPrefab;

    private string[] arrayCharacter = {"F", "U", "S", "R", "O", "D", "A", "H"};
    private List<string> bonusList;
    private List<string> pocket;
    private string[] arrayMiniGame = {"Rock", "Paper", "Scissors"};

    private int bonusCount = 1;
    // private int monsterCount = 0;

    private string text = "        ";

    private float score = 0f;
    private float prevScore = 0;
    private float jumpPower = 15f;
    private float rndRangeStart = 0.5f;
    private float rndRangeEnd = 1f;

    private int isLastChance = 0;
    private int spaceYAxis = 3;

    void Awake() {
        bonusList = new List<string>(arrayCharacter);
        pocket = new List<string>(arrayCharacter);
        monsterPrefab = slimePrefab;

        if (PlayerPrefs.HasKey("IsLastChance")) {
            isLastChance = PlayerPrefs.GetInt("IsLastChance");
        }

        if (PlayerPrefs.HasKey("ScoreValue")) {
            prevScore = PlayerPrefs.GetInt("ScoreValue");
            prevScoreText.text = prevScore.ToString();
        }

        if (PlayerPrefs.HasKey("BonusValue")) {
            bonusCount = PlayerPrefs.GetInt("BonusValue");
        }

        GenerateBonus();

        if (isLastChance == 1) {
            bgMusic2.Stop();
            bgMusic.Play();
        }
    }

    void Start() {
        sc = FindObjectOfType<SceneConntroller>();
    
        centerBackground.GetComponent<MeshRenderer>().material = soilMaterial;
        upperBackground.GetComponent<MeshRenderer>().material = soilMaterial;
        lowerBackground.GetComponent<MeshRenderer>().material = soilMaterial;

        mixer.SetFloat("SFX_VOL", PlayerPrefs.GetFloat("SFX_VOL"));
        mixer.SetFloat("BG_VOL", PlayerPrefs.GetFloat("BG_VOL"));
    }

    void Update() {
        if (pocket.Count == 0) {
            bonusCount += 1;
            pocket = new List<string>(arrayCharacter);
            text = "        ";
            bonusText.text = text;
            bonusSfx.Play();
        }
        bonusCountText.text = "x " + bonusCount;

        if (score + prevScore >= 300) {
            monsterPrefab = ghostPrefab;
            spaceYAxis = 6;
            jumpPower = 20f;
            rndRangeStart = 0.5f;
            rndRangeEnd = 2f;
            centerBackground.GetComponent<MeshRenderer>().material = galaxyMaterial1;
            upperBackground.GetComponent<MeshRenderer>().material = galaxyMaterial2;
            lowerBackground.GetComponent<MeshRenderer>().material = galaxyMaterial2;
        } else if (score + prevScore >= 200) {
            monsterPrefab = batPrefab;
            spaceYAxis = 4;
            jumpPower = 17f;
            rndRangeStart = 0.5f;
            rndRangeEnd = 1f;
            centerBackground.GetComponent<MeshRenderer>().material = skyMaterial1;
            upperBackground.GetComponent<MeshRenderer>().material = skyMaterial2;
            lowerBackground.GetComponent<MeshRenderer>().material = skyMaterial2;
        }
    }

    public void GenerateBonus() {
        for (int i = 0; i < bonusList.Count; i++) {
            string character = bonusList[i];
            bonusTextPrefab.GetComponent<TextMesh>().text = character;
            Instantiate(bonusTextPrefab, new Vector3(Random.Range(-3.12f, 3.12f), 5 + (i * Random.Range(2f, 8f)), 0.6f), Quaternion.identity);
            // Instantiate(bonusTextPrefab, new Vector3(0, i + 1, 0.6f), Quaternion.identity);
        }
    }

    public void LastChance() {
        if (isLastChance == 0) {
            PlayerPrefs.SetInt("IsLastChance", 1);
            PlayerPrefs.SetInt("ScoreValue", (int)score);
            PlayerPrefs.SetInt("BonusValue", bonusCount);
            lastChanceCanvas.SetActive(true);
        } else {
            score += prevScore;
            PlayerPrefs.SetInt("IsLastChance", 0);
            PlayerPrefs.SetInt("ScoreValue", (int)score);
            PlayerPrefs.SetInt("BonusValue", bonusCount);
            sc.LoadScene("GameOver");
        }
    }

    public void NextButtonHandler() {
        if (titleText.text == "Win") {
            sc.LoadScene("GamePlay");
        } else if (titleText.text == "Lose" || titleText.text == "Last Chance") {
            PlayerPrefs.SetInt("ScoreValue", (int)score);
            PlayerPrefs.SetInt("BonusValue", bonusCount);
            sc.LoadScene("GameOver");
        }
    }

    public void PlayLastChance(string selected) {
        int selectedNum = int.Parse(selected);
        int rnd = Random.Range(0, 3);

        if (rnd > selectedNum) {
            titleText.text = "Win";
            nextBtnText.text = "Continue";
            winSfx.Play();
            minigameButtons.SetActive(false);
        } else if (rnd < selectedNum) {
            titleText.text = "Lose";
            loseSfx.Play();
            minigameButtons.SetActive(false);
        } else {
            titleText.text = "Draw";
        }

        // resultText.text = "You: " + arrayMiniGame[selectedNum] + "\nvs\n" + "Opponent: " + arrayMiniGame[rnd];
    }

    public void StartSpawnMonster(GameObject monster) {
        // if (monsterCount < 10) {
            Instantiate(monster, new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (6 + Random.Range(0.5f, 1)), 0), Quaternion.Euler(0, 180, 0));
            // SetMonsterCount(1);
        // }
    }

    public void SetScore(float scorePosY) {
        this.score = Mathf.Round(scorePosY);
        scoreText.text = score.ToString();
    }

    public float GetScore() {
        return this.score;
    }

    public void SetBonusText(string text) {
        bonusText.text = text;
    }

    public void SetText(string text) {
        this.text = text;
    }

    public string GetText() {
        return text;
    }

    public List<string> GetBonusList() {
        return this.bonusList;
    }

    public void SetPocket(List<string> pocket) {
        this.pocket = pocket;
    }

    public List<string> GetPocket() {
        return this.pocket;
    }

    public GameObject GetPlayer() {
        return this.player;
    }

    public GameObject GetFloorPrefap() {
        return this.floorPrefab;
    }

    public GameObject GetSuperFloorPrefap() {
        return this.superFloorPrefab;
    }

    public GameObject GetBonusPrefap() {
        return this.bonusTextPrefab;
    }

    public GameObject GetMonsterPrefap() {
        return this.monsterPrefab;
    }

    // public void SetMonsterCount(int value) {
    //     this.monsterCount += value;
    // }

    // public int GetMonsterCount() {
    //     return this.monsterCount;
    // }

    public int GetSpaceYAxis() {
        return this.spaceYAxis;
    }

    public float GetJumpPower() {
        return this.jumpPower;
    }

    public float GetRndRangeStart() {
        return this.rndRangeStart;
    }

    public float GetRndRangeEnd() {
        return this.rndRangeEnd;
    }
}
