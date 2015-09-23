using UnityEngine;
using System.Collections;

public class DoubleShotBehaviour : MonoBehaviour {
	// camera info
	float camH, camW;
	//Movement
	public float speed = 0.5f;
	
	void Start ()
	{
		//Need camera bounds for bullet removal
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		
		InvokeRepeating("OffCamera", 5f, 2f); // check off-screen after 5 sec
		
		// obtain rigid body for physics
		this.GetComponent<Rigidbody>().velocity = new Vector3 (speed, speed, 0f);
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
		if(coll.gameObject.tag == "Ground"){
			Destroy(this.gameObject);
		}
	}
	
	public void OffCamera()
	{
		if (this.transform.position.x >= (camW / 2 + 50) || this.transform.position.x <= (-camW / 2 - 50))
		{
			Destroy(this.gameObject);
		}
	}
	
}
