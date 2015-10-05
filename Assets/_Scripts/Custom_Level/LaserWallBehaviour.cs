using UnityEngine;
using System.Collections;

public class LaserWallBehaviour : MonoBehaviour {

    Rigidbody laser;

    public float speed = 3f;
    public bool isBlue;         // for collision checking w/ player
    public bool isSpinner;      // if the laser is part of a spinnin-pair

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
        if (isSpinner)  {this.transform.Rotate(Time.deltaTime * new Vector3(0f, 0f, -20 * speed));}
    }


    void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 45) || this.transform.position.x <= (-camW / 2 - 45))
        {
            Destroy(this.gameObject);
        }
    }
}
