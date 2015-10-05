using UnityEngine;
using System.Collections;

public class buzzShotBehaviour : MonoBehaviour {
    // camera info
    float camH, camW;
    // enemy bullet speed
    float speed = 5f;

    Rigidbody bulletRigid;

    public bool isBlue;

    // Use this for initialization
    void Start ()
    {
        //Camera Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;

        float yRand = Random.Range(-0.1f, 0.1f);
        Vector3 bulletDir = new Vector3(-1f, yRand, 0f);

        bulletRigid = this.GetComponent<Rigidbody>();
        this.bulletRigid.velocity = bulletDir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        OffCamera();
    }

//	void OnTriggerEnter(Collider coll){
//		if (coll.gameObject.tag == "Laser")
//		{
//			bool laserBlue = (coll.gameObject.GetComponent<LaserWallBehaviour>().isBlue);
//			if (isBlue != laserBlue)
//			{
//				Destroy(this.gameObject);
//			}
//		}
//	}

    // function to test if to the left or right, OR ABOVE AND BELOW 
    // the camera bounds (enemy and playerShot tests dont check above/below)
    public void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 5) || this.transform.position.x <= (-camW / 2 - 5) || this.transform.position.y >= (camH / 2 + 5) || this.transform.position.y <= (-camH / 2 - 5))
        {
            Destroy(this.gameObject);
        }
    }
}
