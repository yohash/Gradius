using UnityEngine;
using System.Collections;

public class Missile2DColl : MonoBehaviour {

	GameObject missile;

	bool has_coll = false;

	// Use this for initialization
	void Start () {
		missile = this.transform.parent.gameObject;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(has_coll == false && coll.gameObject.tag == "Ground"){
			missile.transform.Rotate(new Vector3(0,0,45));
			missile.GetComponent<Rigidbody>().velocity = new Vector3(missile.GetComponent<MissileBehaviour>().speed,0f,0f);
			has_coll = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = missile.transform.position;
	}
}
