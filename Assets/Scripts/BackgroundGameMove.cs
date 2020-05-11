using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGameMove : MonoBehaviour
{
    public Transform centerBackground;

    void Start() {

    }

    void Update()
    { 
        if (transform.position.y >= centerBackground.position.y + 39.66f) {
            centerBackground.position = new Vector3(centerBackground.position.x, transform.position.y + 39.66f, 6.5f);
        // } else if (transform.position.y <= centerBackground.position - 18.39f) {
        //     centerBackground.position = new Vector3(centerBackground.position.x, transform.position.y - 18.39f, 0f);
        }
    }
}
