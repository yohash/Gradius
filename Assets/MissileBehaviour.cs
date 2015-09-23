using UnityEngine;
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
		this.bulletRigid.velocity = new Vector3(speed,-speed,0f);
	}
	
	// Update is called once per frame
	void Update () {
		OffCamera();
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<BasicEnemyBehaviour>().Scored(); //Add Score
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
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
}
