using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour {

	public float speed = 1f;
	float camH, camW;

    bool __________________;

    Vector3 camPos;
    float camX;
    Camera cam;

	public bool move = true;

    void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		camH /= 2;
		camW /= 2;

        // get camera position to scale the update function

        //Set Star velocity
        this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	        camX = cam.transform.position.x;    // get current camera.x to provide offest for re-spawning stars
	        Vector3 pos = this.transform.position;

	        // move star if they are off-screen
	        if (pos.x < (camX - camW))
	        {
	            pos.x = (camW + camX) + 1f;
	            pos.y = camH * Random.Range(-1f, 1f);
	            this.gameObject.transform.position = pos;
	        }
    }

	//Reset after moving off screen
	void OnBecameInvisible()
    {
        // onBecameInvisible was failing to identify many stars
        // when the camera was moving. Placing the star-movement-function
        // in Update() and subtracting the camera location performs more 
        // accurately so far.
	}
}
