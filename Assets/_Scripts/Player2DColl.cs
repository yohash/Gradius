﻿using UnityEngine;
using System.Collections;

public class Player2DColl : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Volcano"){
			player.GetComponent<PlayerController>().Crash();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = player.transform.position;
	}
}
