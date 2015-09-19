using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//#######
//####### Be aware that this script does not represent any particular enemy
//#######

//Enumerator for the enemy state (fairly useless currently)
enum movementState{forward, backward};

public class BasicEnemyBehaviour : MonoBehaviour {
	//Movement
	public float speed = 0.5f;
	Rigidbody enemyRigid;
	movementState curr = movementState.forward;
	public Vector2 pos1;
	float camH, camW;

	//Scoring
	public int value = 100;
	Text score;

	//Powerup Drops
	public GameObject powerUp;
	public float chance = 1f; //<-------- Currently set to 100% drop chance
	
	void Start () {
		//Variable Initialization
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		score = GameObject.Find ("Score").GetComponent<Text> ();

		//Initially Moving to the Left
		enemyRigid = this.GetComponent<Rigidbody> ();
		enemyRigid.velocity = new Vector3 (-speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//check if the enemy has made it to the pos1 position
		if (this.transform.position.x <= pos1.x && curr == movementState.forward) {
			//Change state, and then reverse direction
			curr = movementState.backward;
			enemyRigid.velocity = new Vector3 (speed, 0f, 0f);
		} else if (curr == movementState.backward && this.transform.position.x >= camW / 2) {
			//Destroy the object once off screen
			Destroy(this.gameObject);
		}
	}

	//Scoring
	public void Scored(){
		//Add enemy value to scoreboard
		score.text = (int.Parse (score.text) + value).ToString ();

		//PowerUp dropping
		if (Random.value < chance) {
			GameObject pow = Instantiate(powerUp) as GameObject;
			pow.transform.position = this.transform.position;
		}
	}
}
