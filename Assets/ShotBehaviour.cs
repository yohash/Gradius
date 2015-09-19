using UnityEngine;
using System.Collections;

public class ShotBehaviour : MonoBehaviour {

	//Movement
	public float speed = 0.5f;
	
	void Start () {
		this.GetComponent<Rigidbody>().velocity = new Vector3 (speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Collision Detection
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<BasicEnemyBehaviour>().Scored(); //Add Score
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
		}
	}
}
