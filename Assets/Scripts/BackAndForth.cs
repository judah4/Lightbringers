using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {

    public float delta = 10.0f;  // Amount to move left and right from the start point
    public float speed = 2.0f; //speed
    private Vector3 startPos; //holds sytarting position

    void Start()
    {
        startPos = transform.position; //sets starting position
    }

    void Update()
    {
        Vector3 v = startPos; 
        v.x += delta * Mathf.Sin(Time.time * speed); 
        transform.position = v;
    }
}
