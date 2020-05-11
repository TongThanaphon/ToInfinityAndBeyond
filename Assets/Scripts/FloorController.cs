using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FloorController : MonoBehaviour
{

    private GamePlayController gpc;

    public AudioSource jumpSfx;

    void Start() {
        gpc = FindObjectOfType<GamePlayController>();
    }

    private void OnCollisionEnter(Collision col) {
        Collider collider = GetComponent<Collider>();
        float normalJump = gpc.GetJumpPower();
        float superJump = gpc.GetJumpPower() + 5f;

        if (col.gameObject.GetComponent<Rigidbody>().velocity.y <= 0) {
            if (collider.name.StartsWith("Floor")) {
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * normalJump, ForceMode.Impulse);
            } else if (collider.name.StartsWith("Super")) {
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * superJump, ForceMode.Impulse);
            }
            jumpSfx.Play();
        }

        if (this.gameObject.transform.position.y >= col.gameObject.transform.position.y) {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider col) {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

}
