using UnityEngine;
using System.Collections;

public class lavaBombBehaviour : MonoBehaviour
{
    Vector3 shotDir = Vector3.zero;

    // camera info
    float camH, camW;

    // enemy bullet speed
    float speed = 26f;

    Rigidbody lavaRigid;

    // Use this for initialization
    void Start ()
    {
        //Camera Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;

        shotDir.y = 1f;
        shotDir.x = Random.Range(-0.3f, 0.3f);

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
}
