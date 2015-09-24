﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HopperBehaviour : BasicEnemyBehaviour
{
    Rigidbody enemyHopper;

    float hSpeed = 8f;      // horizontal speed with which hoppers jump
    float jSpeed = 18f;      // vertical jump speed
    int jumpMax = 6;        // after a certain number of jumps, the NES Gradius
    int jumpCnt = 0;        // hoppers seem to go away

	// Use this for initialization
	void Start ()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();
        enemyHopper = this.GetComponent<Rigidbody>();
        InvokeRepeating("Fire", 6f, 3f);
	}

    public override void Move()
    {
        if (enemyHopper.transform.position.y <= -6.5f) {
            Vector3 tempLoc = Vector3.zero;
            tempLoc.x = enemyHopper.transform.position.x;
            tempLoc.y = -6.5f;
            enemyHopper.transform.position = tempLoc;

            // get player's locaton
            Rigidbody playerTarget = GameObject.Find("Player").GetComponent<Rigidbody>();
            if(enemyHopper.transform.position.x <= playerTarget.transform.position.x && jumpCnt < jumpMax) {
                enemyHopper.velocity = new Vector3(hSpeed, jSpeed, 0);
                jumpCnt++;
            } else if (enemyHopper.transform.position.x > playerTarget.transform.position.x && jumpCnt < jumpMax) {
                enemyHopper.velocity = new Vector3(-hSpeed, jSpeed, 0);
                jumpCnt++;
            } else {
                enemyHopper.velocity = new Vector3(-hSpeed, jSpeed, 0);
            }
        }
    }
}
