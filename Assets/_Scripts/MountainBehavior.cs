﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MountainBehavior : BasicEnemyBehaviour
{
    // turret speed MUST match the speed of the floor
    public float speed = 3f;

    public bool isVolcano = false;

    // Use this for initialization
    void Start()
    {

    }

    public override void Move()
    {
        if(!isVolcano)
        {
            this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        }
    }

    void callFire()
    {
        Fire();
    }
}