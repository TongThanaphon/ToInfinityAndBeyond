using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    private GamePlayController gpc;

    public AudioSource deathSfx;

    private Rigidbody rb;
    private float moveInput;
    private float speed = 8f;
    private bool isAttack = false;


    void Start() {
        rb = GetComponent<Rigidbody>();
        gpc = FindObjectOfType<GamePlayController>();
    }

    void Update() {
        float score = gpc.GetScore();
        if (rb.velocity.y > 0 && transform.position.y > score) {
            gpc.SetScore(transform.position.y);
        }

        if (rb.velocity.y < -15f) {
            PlayerDie();
        }
    }

    void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * speed, rb.velocity.y, rb.velocity.z);
        //transform.Translate(moveInput * Time.deltaTime * -speed, 0, 0);
    }

    public void PlayerDie() {
        gpc.LastChance();
        rb.isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
        deathSfx.Play();
    }

    public bool GetIsAttack() {
        return this.isAttack;
    }

}
