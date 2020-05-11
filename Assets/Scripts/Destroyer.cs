using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
    private GamePlayController gpc;

    private GameObject player;
    private GameObject floorPrefab;
    private GameObject superFloorPrefab;
    private GameObject bonusTextPrefab;
    private GameObject monsterPrefab;

    private float score;

    void Start() {
        gpc = FindObjectOfType<GamePlayController>();
        player = gpc.GetPlayer();
        floorPrefab = gpc.GetFloorPrefap();
        superFloorPrefab = gpc.GetSuperFloorPrefap();
        bonusTextPrefab = gpc.GetBonusPrefap();
    }

    void Update() {
        monsterPrefab = gpc.GetMonsterPrefap();
    }

    private void OnTriggerEnter(Collider col) {
        GameObject gameObject = col.gameObject;
        int rnd = Random.Range(1, 15);
        int spaceYAxis = gpc.GetSpaceYAxis();
        float rndRangeStart = gpc.GetRndRangeStart();
        float rndRangeEnd = gpc.GetRndRangeEnd();

        if (rnd == 1) {
            List<string> list = gpc.GetBonusList();
            int index = Random.Range(0, list.Count);
            string character = list[index];
            bonusTextPrefab.GetComponent<TextMesh>().text = character;
            Destroy(gameObject);
            Instantiate(bonusTextPrefab, new Vector3(Random.Range(-3.12f, 3.12f), spaceYAxis + 2 + (player.transform.position.y * Random.Range(2f, 8f)), 0.6f), Quaternion.identity);

        }

        if (gameObject.name.StartsWith("Floor")) {
            if (rnd == 1) {
                Destroy(gameObject);
                Instantiate(superFloorPrefab, new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + Random.Range(rndRangeStart, rndRangeEnd)), 0), Quaternion.identity);

                gpc.StartSpawnMonster(monsterPrefab);

            } else {
                gameObject.transform.position = new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + Random.Range(rndRangeStart, rndRangeEnd)), 0);
            }
        } else if (gameObject.name.StartsWith("Super")) {
            if (rnd == 1) {
                gameObject.transform.position = new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + Random.Range(rndRangeStart, rndRangeEnd)), 0);
            } else {
                Destroy(gameObject);
                Instantiate(floorPrefab, new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + Random.Range(rndRangeStart, rndRangeEnd)), 0), Quaternion.identity);
            }
        } else if (gameObject.name.StartsWith("Bonus Text")) {
            gameObject.transform.position = new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + 5 + Random.Range(rndRangeStart, rndRangeEnd)), 0.6f);
        } else if (gameObject.name.StartsWith("Monster")) {
                if (rnd == 1) {
                    gameObject.transform.position = new Vector3(Random.Range(-3.12f, 3.12f), player.transform.position.y + (spaceYAxis + 3 + Random.Range(rndRangeStart, rndRangeEnd)), 0);
                } else {
                    // gpc.SetMonsterCount(-1);
                    gpc.StartSpawnMonster(monsterPrefab);
                    Destroy(gameObject);
                }
        }
    }

}
