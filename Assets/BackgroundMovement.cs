using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour {

	public float speed = 10f;
	float camH, camW;
	
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		camH /= 2;
		camW /= 2;

		//Set Star velocity
		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Reset after moving off screen
	void OnBecameInvisible(){
		Vector3 pos = this.transform.position;
		pos.x = camW + 1f;
		pos.y = camH * Random.Range (-1f, 1f);
		this.gameObject.transform.position = pos;
	}
}
