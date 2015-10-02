using UnityEngine;
using System.Collections;
using System;

public class LaserExtend : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}

	void Extend (){
		RaycastHit2D[] hits;
		hits = Physics2D.RaycastAll(Vector3.zero, transform.up, 20F);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit2D hit = hits[i];
			if (hit.collider.gameObject.tag == "Ground") {
				this.transform.localScale = new Vector3(.25f,hit.distance/2, 1f);
				this.transform.localPosition =	new Vector3(0f,hit.distance/2,0f);
				return;
			}
		}
		this.transform.localScale = new Vector3(.25f,20f, 1f);
		this.transform.localPosition = new Vector3(0f,10f,0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Extend ();

	}
}
