using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHomeMove : MonoBehaviour
{

    private float target;

    void Start()
    {
        
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target), 0.05f);
    }
}
