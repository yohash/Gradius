using UnityEngine;
using System.Collections;

public class Laser2DColl : MonoBehaviour {

    GameObject playerLaser;

    // Use this for initialization
    void Start()
    {
        playerLaser = this.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Mountain")
        {
            playerLaser.GetComponent<LaserBehaviour>().hitMountain();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = playerLaser.transform.position;
    }
}
