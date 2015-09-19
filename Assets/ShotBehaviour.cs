using UnityEngine;
using System.Collections;

public class ShotBehaviour : MonoBehaviour {

	//Movement
	public float speed = 0.5f;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().velocity = new Vector3 (speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<BasicEnemyBehaviour>().Scored();
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
		}
	}
}
