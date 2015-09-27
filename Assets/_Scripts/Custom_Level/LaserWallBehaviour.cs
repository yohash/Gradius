﻿using UnityEngine;
using System.Collections;

public class LaserWallBehaviour : MonoBehaviour {

    Rigidbody laser;

    public float speed = 3f;
    public bool isBlue;

    float camW, camH;

	// Use this for initialization
	void Start () {

    }

    void Update() {
        Move();
        OffCamera();
    }

    void Move()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
    }


    void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 25) || this.transform.position.x <= (-camW / 2 - 25))
        {
            Destroy(this.gameObject);
        }
    }
}
