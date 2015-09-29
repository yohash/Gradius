using UnityEngine;
using System.Collections;

public class enemyShotBehavior : MonoBehaviour {
    // camera info
    float camH, camW;

    // enemy bullet speed
	float speed = 5f;

	Rigidbody bulletRigid;

	// Use this for initialization
	void Start ()
    {
        //Camera Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;

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
        OffCamera();
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

    void onTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Ground") { Destroy(this.gameObject); }
    }
}
