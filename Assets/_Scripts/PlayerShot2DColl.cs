﻿using UnityEngine;
using System.Collections;

public class PlayerShot2DColl : MonoBehaviour {

    GameObject playerShot;

    // Use this for initialization
    void Start()
    {
        playerShot = this.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Mountain")
        {
            playerShot.GetComponent<ShotBehaviour>().hitMountain();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = playerShot.transform.position;
    }
}
