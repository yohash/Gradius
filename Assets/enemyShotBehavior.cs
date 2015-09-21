using UnityEngine;
using System.Collections;

public class enemyShotBehavior : MonoBehaviour {

	float speed = 5f;

	Rigidbody bulletRigid;

	// Use this for initialization
	void Start () {
		// get player's locaton
		Rigidbody playerTarget = GameObject.Find ("Player").GetComponent<Rigidbody> ();

		// vector subtraction to get vector (enemy -> player)
		Vector3 playerPos = playerTarget.transform.position;
		Vector3 bulletDir = playerPos - this.transform.position;

		bulletRigid = this.GetComponent<Rigidbody> ();
		this.bulletRigid.velocity = bulletDir.normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
