using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum ATSTstatus{ seeking, firing, chill, leaving};

public class ATSTBehaviour : BasicEnemyBehaviour
{
    public float speed = 3f;                // speed MUST match the speed of the floor
    public float seekSpeed = 10f;
    public float shotDelay = 2f, startDelay;
    public float chillDelay = 4f;           // how long it waits after firing

    public int shotCnt, maxShots = 4;        // total number of shot "attempts"
    public float fireChance = 0.25f;         // the ships sometimes dont fire, I cant tell 
                                            // why, so we'll make it random
    Rigidbody enemyRigid;
    Vector3 playerPos;

    private Vector3 targetX = Vector3.zero;
    private ATSTstatus thisStatus = ATSTstatus.seeking;
    
    // Use this for initialization
    void Start ()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();
        enemyRigid = this.GetComponent<Rigidbody>();

        shotCnt = 0;

        getPlayerPos();     // initialize enemy speed and 1st go-to position
        print("SEEKING");
    }

    public override void Move()
    {        
        // set ATST actions
        if (thisStatus == ATSTstatus.seeking)
        {
            // check to see if enemy reached its landmark within +/-0.1
            if (this.transform.position.x < targetX.x + 0.25 && this.transform.position.x > targetX.x - 0.25) {
                enemyRigid.velocity = new Vector3(-speed, 0, 0);
                startDelay = Time.time;
                thisStatus = ATSTstatus.firing;
                print("FIRING");
            }
        }
        else if (thisStatus == ATSTstatus.firing && Time.time - startDelay > shotDelay)
        {
            // if the delay in shotDelay has passed
            shotCnt++;
            if (shotCnt > maxShots) { 
                thisStatus = ATSTstatus.leaving;
                enemyRigid.velocity = new Vector3(-seekSpeed, 0, 0);
            } else if (Random.value < fireChance) { 
                Fire();
                startDelay = Time.time;
                thisStatus = ATSTstatus.chill;
                print("Chillin...");
            } else {
                getPlayerPos();
                thisStatus = ATSTstatus.seeking;
            }
        }
        else if (thisStatus == ATSTstatus.chill && Time.time - startDelay > chillDelay) {
            thisStatus = ATSTstatus.seeking;
            getPlayerPos();   // restart the cycle
            print("SEEKING");
        } else if (thisStatus == ATSTstatus.leaving) {

        }
    }

    private void getPlayerPos()
    {
        playerPos = GameObject.Find("Player").GetComponent<Rigidbody>().position;

        // compute 45 degrees from player ship
        if (this.transform.position.y >= 0)
        {
            targetX.x = playerPos.x + (9.5f - playerPos.y);     // since we seek a 45d angle, dY = dX
        }
        else
        {
            targetX.x = playerPos.x + (playerPos.y + 7);
        }

        // send the player towards this landmark
        if (this.transform.position.x >= targetX.x) {
            enemyRigid.velocity = new Vector3(-seekSpeed, 0, 0);
        } else {
            enemyRigid.velocity = new Vector3(seekSpeed, 0, 0);
        }
    }
}
