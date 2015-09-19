using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Enumerator for our two-tier PowerUp System
enum powerLevel{first, second};

public class PlayerController : MonoBehaviour {

	//UI elements
	public int health = 3;
	Text healthText;

	//Player Control
	public Vector2 maxSpeed;
	Rigidbody shipRigid;
	float camW, camH;

	//Shooting Mechanics 
	public GameObject shotPrefab;
	public Vector3 shotspawn = new Vector3(1.5f, 0f, 0f);
	public float reload;
	float currReload;

	//PowerUps
	powerLevel pow = powerLevel.first;
	Image img;
	
	void Start () {
		//Initiazlize variables
		shipRigid = this.gameObject.GetComponent<Rigidbody> ();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;

		//Set the text for player health
		healthText = GameObject.Find ("Health").GetComponent<Text> ();
		healthText.text = health.ToString();

		img = GameObject.Find ("Power").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 speed = Vector3.zero;

		//Reload/Firing timing
		if (currReload < Time.deltaTime) {
			currReload = 0f;
		} else {
			currReload -= Time.deltaTime;
		}

		//Reset the game if player health drops to 0
		if (health <= 0) {
			Application.LoadLevel("Scene_0");
		}

		//Player movement, includes screen edge bounding
		if(Input.GetKey(KeyCode.W) && (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y/2 < camH/2)){
			speed.y += maxSpeed.y;
		}

		if(Input.GetKey(KeyCode.S) && (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y/2 > -camH/2)){
			speed.y -= maxSpeed.y;
		}
		
		if(Input.GetKey(KeyCode.A) && (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/2 > -camW/2)){
			speed.x -= maxSpeed.x;
		}
		
		if(Input.GetKey(KeyCode.D) && (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/2 < camW/2)){
			speed.x += maxSpeed.x;
		}

		shipRigid.velocity = speed;

		//Shooting
		if (Input.GetKey (KeyCode.Space) && currReload <= 0) {
			//Check for reload and Power Level
			if(pow == powerLevel.first){
				currReload = reload;
			}
			if(pow == powerLevel.second){
				currReload = reload/4;
			}
			//Create bullet and move it to the player position
			GameObject shot = Instantiate(shotPrefab) as GameObject;
			shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
		}
	}

	//Collision detection
	void  OnTriggerEnter(Collider coll){
		//Enemy Collision
		if (coll.gameObject.tag == "Enemy") {
			health--;
			healthText.text = health.ToString();

			pow = powerLevel.first;
			img.color = Color.white;
		}
		//PowerUp Collision
		if (coll.gameObject.tag == "PowerUp") {
			pow = powerLevel.second;
			img.color = Color.red;
			Destroy (coll.gameObject);
		}
	}
}
