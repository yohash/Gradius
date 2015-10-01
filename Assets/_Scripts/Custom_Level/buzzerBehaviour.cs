using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum buzzSt { enter, fire }

public class buzzerBehaviour : BasicEnemyBehaviour
{
    float speed = 15f;      // speed that buzzers enter/exit

    Rigidbody enemyRigid;
    private buzzSt buzzState;

	// Use this for initialization
	void Start () {
        buzzState = buzzSt.enter;

        enemyRigid = this.GetComponent<Rigidbody>();
        this.enemyRigid.velocity = new Vector3(-speed, 0f, 0f);

        base.score = GameObject.Find("Score").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x < 10f && buzzState == buzzSt.enter)
        {
            this.enemyRigid.velocity = Vector3.zero;
            buzzState = buzzSt.fire;
            Invoke("shoot", 1.5f);
        }
    }

    void shoot()
    {
        this.enemyRigid.velocity = new Vector3(speed, 0f, 0f);
        Fire();
    }
}
