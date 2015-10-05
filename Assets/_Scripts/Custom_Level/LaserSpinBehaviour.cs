using UnityEngine;
using System.Collections;

public class LaserSpinBehaviour : MonoBehaviour {

    // *************************************************
    //
    //  this code obsolete due to changes in 
    //     laserwallbehaviour that include spinner code
    //      (the algo initiated here)
    //      wrapped in an isSpinner bool
    //
    //***************************************************
    Rigidbody laser;

    public float speed = 3f;
    public bool isBlue;

    float camW, camH;

	// Use this for initialization
	void Start () {

    }

    void FixedUpdate() {
        Move();
        OffCamera();
    }

    void Move()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
		this.transform.Rotate(Time.deltaTime * new Vector3(0f, 0f, -20*speed));
      //  print("spinning");
    }

    void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 25) || this.transform.position.x <= (-camW / 2 - 25))
        {
            Destroy(this.gameObject);
        }
    }
}
