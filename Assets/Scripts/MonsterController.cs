using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MonsterController : MonoBehaviour
{

    private GamePlayController gpc;
    private PlayerController player;

    public AudioSource attackSfx;

    void Start() {
        gpc = FindObjectOfType<GamePlayController>();
        player = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                if (hit.collider.name.StartsWith("Monster")) {
                    attackSfx.Play();
                    hit.collider.gameObject.SetActive(false);
                    gpc.StartSpawnMonster(hit.collider.gameObject);
                    // gpc.SetMonsterCount(-1);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col) {
        GameObject gameObject = col.gameObject;

        if (gameObject.name.StartsWith("Player")) {
            player.PlayerDie();
            gpc.LastChance();
        }
    }

}
