using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum darterMotion { first, second };
enum darterLoc { top, bot}

public class Darters : BasicEnemyBehaviour
{
    public GameObject silo;     // the silo which spawns Darters
    private darterMotion state = darterMotion.first;
    private darterLoc dLoc = darterLoc.top;

    // speed of background
    float speed = 3f;
    float vSpeed = 12f;         // vertical speed 
    float dartSpeed = 15f;      // darting speed

	// Use this for initialization
	void Start ()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();
        if (this.transform.position.y < 0) { dLoc = darterLoc.bot; }
    }

    public override void Move()
    {
        Rigidbody playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();

        if (dLoc == darterLoc.top)
        {
            // run code for silos on top
            if (state == darterMotion.first && this.transform.position.y > playerRigid.transform.position.y)
            {
                this.transform.position += Time.deltaTime * new Vector3(-speed, -vSpeed, 0f);
            }
            else if (state == darterMotion.first && this.transform.position.y <= playerRigid.transform.position.y)
            {
                Vector3 dartLoc = Vector3.zero;
                dartLoc.y = playerRigid.transform.position.y;
                dartLoc.x = this.transform.position.x;
                this.transform.position = dartLoc;
                state = darterMotion.second;
            }
        }
        else
        {
            // run code for silos on the bottom
            if (state == darterMotion.first && this.transform.position.y < playerRigid.transform.position.y)
            {
                this.transform.position += Time.deltaTime * new Vector3(-speed, vSpeed, 0f);
            }
            else if (state == darterMotion.first && this.transform.position.y >= playerRigid.transform.position.y)
            {
                Vector3 dartLoc = Vector3.zero;
                dartLoc.y = playerRigid.transform.position.y;
                dartLoc.x = this.transform.position.x;
                this.transform.position = dartLoc;
                state = darterMotion.second;
            }
        }
        // move darter forward in second state
        if (state == darterMotion.second)
        {
            this.transform.position += Time.deltaTime * new Vector3(-dartSpeed, 0f, 0f);
        }

    }
}
