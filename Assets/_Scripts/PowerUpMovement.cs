using UnityEngine;
using System.Collections;

public class PowerUpMovement : MonoBehaviour
{
    GameObject pUp;

    // camera info
    float camH, camW;
    float speed = 3f;    // speed of the map moving  
                         // Use this for initialization
    void Start ()
    {
        //Camera Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;

        pUp = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        pUp.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        OffCamera();
    }

    public void OffCamera()
    {
        if (this.transform.position.x >= (camW / 2 + 2) || this.transform.position.x <= (-camW / 2 - 2))
        {
            Destroy(this.gameObject);
        }
    }
}
