using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum bossState{ entering, fightingUP, pausing, fightingDOWN};

public class BossBehaviour : BasicEnemyBehaviour
{
    AudioSource bossHit;

    public bool customLevel = false;        // if custom level boss, activate lasers

    public float speed = 3f;        // entering speed
    public float fightSpeed = 5f;   // move up-and-down speed

    float topLim = 8f;            // HIGHEST vertical limit
    float botLim = -5f;             // LOWEST vertical limit
    float bossTopLim;           // boss high vertical limit
    float bossBotLim;           // loww lower vertical limit

    float randoPause = 1f;          // the boss pauses sometimes at the top of his
    float pauseTimer;               // "battle rotation"....

    float fireRate = 1.3f;          // shoot every 1.2 sec
    float fireTimer;
    bool firing = false;            // suppress fire() while entering

    private bossState bs = bossState.entering;      // state space var

    // player info for tracking their location
    Rigidbody playerRigid;
    Vector3 playerPos;
    private Vector3 playerLoc = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();
        bossHit = this.GetComponent<AudioSource>();
    }

    public override void Move()
    {
        getPlayerLoc();         // find the players location the set the boss's
                                // upper and lower roam limits

        //      Big ugly if-else chain that  uses state space variables to
        // have the ship scan up and down vertically (y) from the player location.
        //      Fire() timing is independant.
        //      If the player hides in a corner, the ship is supposed to freeze at 
        // top or bottom ~1sec then fire.

        if (bs == bossState.entering && this.transform.position.x >= 7f)
        {   // this controls the boss entering, and starts his fight
            fireTimer = Time.time;
            firing = true;
            this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        }
        else if (bs == bossState.entering && this.transform.position.x <= 7f) { 
            // initiate lasers if custom level
            if (customLevel) { GetComponentInChildren<laserPointerFire>().initiateFire(); }
           // initiate boss fight
            bs = bossState.fightingUP;
        }
        else if (bs == bossState.fightingUP && this.transform.position.y < bossTopLim)
        {   // move boss upwards if he hasnt reached his limit
            this.transform.position += Time.deltaTime * new Vector3(0f, fightSpeed, 0f);
        }
        else if (bs == bossState.fightingUP && this.transform.position.y >= bossTopLim)
        {   // boss reached his upper limit
            this.transform.position = new Vector3(this.transform.position.x, bossTopLim, 0f);

            // trigger a pause if the player is hiding in upper corner
            if (this.transform.position.y + 0.5f >= topLim && playerPos.y >= 7f)
            {
                pauseTimer = Time.time;
                bs = bossState.pausing;
            }
            else
            {   // player isnt hiding... start going downward
                bs = bossState.fightingDOWN;
            }
        }
        else if (bs == bossState.fightingDOWN && this.transform.position.y > bossBotLim)
        {   // move boss downward if he hastn reached his limit
            this.transform.position -= Time.deltaTime * new Vector3(0f, fightSpeed, 0f);
        }
        else if (bs == bossState.fightingDOWN && this.transform.position.y <= bossBotLim)
        {   // boss reached his lower limit
            this.transform.position = new Vector3(this.transform.position.x, bossBotLim, 0f);

            // trigger a pause if the player is hiding in bottom corner
            if (this.transform.position.y - 0.5f <= botLim && playerPos.y <= -4f)
            {
                pauseTimer = Time.time;
                bs = bossState.pausing;
            }
            else
            {   // player isnt hiding... move on up
                bs = bossState.fightingUP;
            }
        }
        else if (bs == bossState.pausing && (Time.time - pauseTimer) > 0.7f)
        {
            // boss has paused 1 sec
            // player is hiding in a corner - pause then shoot at them

            Fire();
            fireTimer = Time.time;      // reset timer to prevent boss ship from "double fire"

            if (this.transform.position.y > 0)
            {   // if at the top, fight on down
                bs = bossState.fightingDOWN;
            }
            else
            {   // if at the bottom, fight on up
                bs = bossState.fightingUP;
            }

        }
    }

    public void getPlayerLoc()
    {
        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerPos = playerRigid.transform.position;

        // calculate boss's upper scan limit
        bossTopLim = Mathf.Clamp(playerRigid.transform.position.y + 8f, botLim, topLim);

        // calculate boss's lower scan limit
        bossBotLim = Mathf.Clamp(playerRigid.transform.position.y - 8f, botLim, topLim);

        //print("TOP LIM = " + bossTopLim + "("+topLim+"), BOT LIM = " + bossBotLim + "(" + botLim + ")");
    }

    // Update is called once per frame
    void Update()
    {
        if (bs != bossState.pausing && (Time.time - fireTimer) > fireRate)
        {
            fireTimer = Time.time;
            Fire();
        }
    }

    public override void Hit()
    {
        if (health > 1)
        {
            health--;
            bossHit.Play((ulong)0.0);
        }
        else
            {
            base.Scored();

            //// play the death audio
            //AudioSource deathSFX = this.GetComponent<AudioSource>();
            //deathSFX.Play((ulong)0.0);

            // create an explosion
            GameObject ex = Instantiate(explosion) as GameObject;
            Vector3 exLoc = Vector3.zero;

            exLoc.x = this.transform.position.x;
            exLoc.y = this.transform.position.y;
            exLoc.z = 5f;

            ex.transform.position = exLoc;

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Scheduler>().youWin();

            Destroy(this.gameObject);
        }
    }
}
