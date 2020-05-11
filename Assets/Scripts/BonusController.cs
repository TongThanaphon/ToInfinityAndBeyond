using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BonusController : MonoBehaviour
{

    private GamePlayController gpc;

    void Start() {
        gpc = FindObjectOfType<GamePlayController>();
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider col) {
        GameObject gameObject = col.gameObject;
        if (gameObject.name == "Player") {
            BonusHandler(gameObject);
            gpc.SetBonusText(gpc.GetText());
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void BonusHandler(GameObject gameObject) {
        string item = this.gameObject.GetComponent<TextMesh>().text;
        int index = gpc.GetBonusList().IndexOf(item);
        StringBuilder sb = new StringBuilder(gpc.GetText());
        sb[index] = item.ToCharArray()[0];
        gpc.SetText(sb.ToString());

        List<string> pocket = gpc.GetPocket();
        pocket.Remove(item);
        gpc.SetPocket(pocket);
    }

}
