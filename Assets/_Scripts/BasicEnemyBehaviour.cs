using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//#######
//####### Be aware that this script does not represent any particular enemy
//#######

//Enumerator for the enemy state (fairly useless currently)
enum movementState{forward, backward};

public class BasicEnemyBehaviour : MonoBehaviour {

	bool dead = false;

	//Movement
	//public float speed = 0.5f;
	//Rigidbody enemyRigid;
	movementState curr = movementState.forward;
	public Vector2 pos1;
	public float camH, camW;

	//Health
	public int health = 1;

	//Scoring
	public int value = 100;
	public Text score;

	//Powerup Drops
	public GameObject powerUp;
	public float chance = 1f; //<-------- Currently set to 100% drop chance

	//enemy bullets
	public GameObject shot;

    //explosion
    public GameObject explosion;   // each explosion has its own attached script that
                                    // terminates the animation after processingS	
	void Start () {
		//Variable Initialization
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		score = GameObject.Find ("Score").GetComponent<Text> ();

        
        //Initially Moving to the Left
        //enemyRigid = this.GetComponent<Rigidbody> ();

        //INITIALIZING SPEED (we dont want this)
        //enemyRigid.velocity = new Vector3 (-speed, 0f, 0f);
    }

	// Update is called once per frame
	public virtual void Move(){
//		if (this.transform.position.x <= pos1.x && curr == movementState.forward) {
//			//Change state, and then reverse direction
//			curr = movementState.backward;
//			enemyRigid.velocity = new Vector3 (speed, 0f, 0f);
//		}
//		else if (curr == movementState.forward){
//			//Nothing, is set in Start
//		}
	}

	public void OffCamera() {
		if (this.transform.position.x >= (camW / 2 + 50) || this.transform.position.x <= (-camW/2 - 50)){
			Destroy (this.gameObject);
		}
	}

	void FixedUpdate () {
		//check if the enemy has made it to the pos1 position
		Move ();

		OffCamera(); // delete enemies
	}

    public virtual void Hit()
    {
        if (this.tag != "Mountain")
        {
            if (health > 1)
            {
                health--;
            }
            else
            {
                Scored();

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
                Destroy(this.gameObject);
            }
        }
    }

	//Scoring
	public void Scored(){
		//Add enemy value to scoreboard
		if(!dead){
			score.text = (int.Parse (score.text) + value).ToString ();

			//PowerUp dropping
			if (chance == 1) {
				GameObject pow = Instantiate(powerUp) as GameObject;
				pow.transform.position = this.transform.position;
			}
			dead = true;
		}
	}

	public void Fire() {
		// this function will get the player position, 
		// - enemyShotBehavior script fires directly at the player

		GameObject enemyShot = Instantiate (shot) as GameObject;		
		enemyShot.transform.position = this.transform.position;	
	}
}
