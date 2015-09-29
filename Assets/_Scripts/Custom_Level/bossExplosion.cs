using UnityEngine;
using System.Collections;

public class bossExplosion : MonoBehaviour {
    float startTime;                // time animation started
    float duration = 1.20f;         // how long this animation takes

    float speed = 3f;           // this speed

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        if (Time.time - duration > startTime) { Destroy(this.gameObject); }
    }
}
