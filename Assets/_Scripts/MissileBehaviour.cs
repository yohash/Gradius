﻿using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {
	// camera info
	float camH, camW;
	
	// enemy bullet speed
	public float speed = 5f;
	
	Rigidbody bulletRigid;
	
	// Use this for initialization
	void Start ()
	{
		//Camera Initialization
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		
		bulletRigid = this.GetComponent<Rigidbody> ();
		this.bulletRigid.velocity = new Vector3(1,-3f,0f).normalized*speed;
	}
	
	// Update is called once per frame
	void Update () {
		OffCamera();
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<BasicEnemyBehaviour>().Hit(); //Add Score
			Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Ground")
        {
            //print("G");
            this.bulletRigid.velocity = new Vector3(speed, 0f, 0f);
        }
    }
	// function to test if to the left or right, OR ABOVE AND BELOW 
	// the camera bounds (enemy and playerShot tests dont check above/below)
	public void OffCamera()
	{
		if (this.transform.position.x >= (camW / 2 + 5) || this.transform.position.x <= (-camW / 2 - 5) || this.transform.position.y >= (camH/2 + 5) || this.transform.position.y <= (-camH/2 -5))
		{
			Destroy(this.gameObject);
		}
	}

    // if the mt gets hit the missile is gone
    public void hitMountain()
    {
        Destroy(this.gameObject);
    }
}
