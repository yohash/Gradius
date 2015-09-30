using UnityEngine;
using System.Collections;

public class treeBehaviour : MonoBehaviour {
    // turret speed MUST match the speed of the floor
    public float speed = 3f;
    public float camH, camW;

    public bool withVolcano = false;

    // Use this for initialization
    void Start()
    {
        //Variable Initialization
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;
    }

    void Update() {
        Move();
        OffCamera();
    }

    public void Move()
    {
        if (!withVolcano) { this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f); }
    }
    public void OffCamera()
    {
        if (this.transform.position.x <= (-camW / 2 - 5))
        {
            Destroy(this.gameObject);
        }
    }
}
