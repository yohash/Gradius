using UnityEngine;
using System.Collections;

public class enemyShot2DColl : MonoBehaviour {

    GameObject bullet;

    // Use this for initialization
    void Start ()
    {
        bullet = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Mountain")
        {
            bullet.GetComponent<enemyShotBehavior>().destroyThisBullet();
        }
    }
}
