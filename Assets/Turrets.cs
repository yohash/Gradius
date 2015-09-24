using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turrets : BasicEnemyBehaviour {

    // turret speed MUST match the speed of the floor
    public float speed = 3f;

    // Use this for initialization
    void Start ()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();
        InvokeRepeating("Fire", 3f, 3f);
    }

    public override void Move()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
    }
}
