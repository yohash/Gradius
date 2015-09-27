using UnityEngine;
using System.Collections;

public class OptionBehaviour : MonoBehaviour {

	GameObject tracker;
	float speed;

	// Use this for initialization
	void Start () {
		if(GameObject.FindGameObjectsWithTag("Option").Length == 1){
			tracker = GameObject.FindGameObjectWithTag("Player");
		} else {
			GameObject[] options = GameObject.FindGameObjectsWithTag("Option");
			for( int i = 0; i < options.Length; i++) {
				if(options[i].gameObject == this.gameObject) continue;
				print ("Boosh");
				tracker = options[i].gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speed = tracker.GetComponent<Rigidbody>().velocity.magnitude;
		if(speed == 0) speed = 8f;
		Vector3 tmp_vel = tracker.GetComponent<Rigidbody>().velocity;
		Vector3 mag = this.transform.position - tracker.transform.position;
		if(Mathf.Abs(mag.magnitude) > 1f){
			Vector3 point = tracker.transform.position - tmp_vel.normalized;
			float step = speed*Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(this.transform.position, point, step);
		}
	}

	public void Fire(GameObject projectile, Vector3 spawnLoc){
		GameObject bullet = Instantiate(projectile) as GameObject;
		bullet.GetComponent<Rigidbody>().MovePosition(this.transform.position + spawnLoc);
	}
}
