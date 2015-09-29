using UnityEngine;
using System.Collections;

public class lavaBombBehaviour : MonoBehaviour
{
    Vector3 shotDir = Vector3.zero;
    //explosion
    public GameObject explosion;

    // camera info
    float camH, camW;

    // enemy bullet speed
    float speed = 28f;

    Rigidbody lavaRigid;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+3.25f, 0f);

        //Camera Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;

        shotDir.y = 1f;
        shotDir.x = Random.Range(-0.2f, 0.2f);

        lavaRigid = this.GetComponent<Rigidbody>();
        this.lavaRigid.velocity = shotDir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        OffCamera();
    }

    // function to test if to the left or right, OR ABOVE AND BELOW 
    // the camera bounds (enemy and playerShot tests dont check above/below)
    public void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 5) || this.transform.position.x <= (-camW / 2 - 5) || this.transform.position.y >= (camH / 2 + 5) || this.transform.position.y <= (-camH / 2 - 5))
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "PlayerShot")
        {
            GameObject ex = Instantiate(explosion) as GameObject;
            Vector3 exLoc = Vector3.zero;

            exLoc.x = this.transform.position.x;
            exLoc.y = this.transform.position.y;
            exLoc.z = 5f;

            ex.transform.position = exLoc;
            Destroy(this.gameObject);
        }
    }
}
