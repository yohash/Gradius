using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Enumerator for our two-tier PowerUp System
enum powerLevel{none, first, second};

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
	powerLevel pow = powerLevel.none;
	int[] powers = {0,0,0,0,0};
	Image[] pow_Img = new Image[5];
	Text[] pow_Lbl = new Text[5];
	
	void Start () {
		//Initiazlize variables
		shipRigid = this.gameObject.GetComponent<Rigidbody> ();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;

		//Set the text for player health
		healthText = GameObject.Find ("Health").GetComponent<Text> ();
		healthText.text = health.ToString();

		//Add the power images/labels to the array
		pow_Img[0] = GameObject.Find("Power_Speed").GetComponent<Image> ();
        pow_Img[1] = GameObject.Find("Power_Missile").GetComponent<Image>();
        pow_Img[2] = GameObject.Find("Power_Double").GetComponent<Image>();
        pow_Img[3] = GameObject.Find("Power_Laser").GetComponent<Image>();
        pow_Img[4] = GameObject.Find("Power_Option").GetComponent<Image>();
        pow_Lbl[0] = GameObject.Find("Power_Speed_Label").GetComponent<Text>();
        pow_Lbl[1] = GameObject.Find("Power_Missile_Label").GetComponent<Text>();
        pow_Lbl[2] = GameObject.Find("Power_Double_Label").GetComponent<Text>();
        pow_Lbl[3] = GameObject.Find("Power_Laser_Label").GetComponent<Text>();
        pow_Lbl[4] = GameObject.Find("Power_Option_Label").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		Vector3 speed = Vector3.zero, camPos = Vector3.zero;
        
        // get current camera data to keep ship on screen
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();     // get camera position to keep player on camera
        camPos = cam.transform.position;
        float camX = camPos.x;
        float camY = camPos.y;

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
		if(Input.GetKey(KeyCode.W) && (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y/2 < (camH/2 + camY))){
			speed.y += maxSpeed.y;
		}
		if(Input.GetKey(KeyCode.S) && (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y/2 > (-camH/2 + camY))){
			speed.y -= maxSpeed.y;
		}		
		if(Input.GetKey(KeyCode.A) && (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/2 > (-camW/2 + camX))){
			speed.x -= maxSpeed.x;
		}		
		if(Input.GetKey(KeyCode.D) && (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/2 < (camW/2 + camX))){
			speed.x += maxSpeed.x;
		}

        // if ship is off screen (-x) add an offset to keep us on screen
        float offset = this.transform.position.x - (camX - camW / 2);
        if(offset < 0) {
            shipRigid.position -= new Vector3(offset, 0, 0);
        } else {
            shipRigid.velocity = speed;
        }

		//Shooting
		if (Input.GetKey (KeyCode.Space) && currReload <= 0) {
			//Check for reload and Power Level
			if(powers[0] == 0){
				currReload = reload;
			}
			if(powers[0] == 1){
				currReload = reload/4;
			}

			//Create bullet and move it to the player position
			GameObject shot = Instantiate(shotPrefab) as GameObject;
			shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
		}

		//Using PowerUp
		if(Input.GetKey(KeyCode.RightAlt) && pow != powerLevel.none)
		{
			if(powers[(int)pow-1] == 0){
				powers[(int)pow-1]++;
				pow_Img[(int)pow-1].color = Color.blue;
				pow_Lbl[(int)pow-1].enabled = false;
				pow = powerLevel.none;
			}
		}
	}

	//Collision detection
	void  OnTriggerEnter(Collider coll){
		//Enemy Collision
		if (coll.gameObject.tag == "Enemy") {
			health--;
			healthText.text = health.ToString();

			//Reset powerups
			resetPowers();
			pow = powerLevel.none;
		}
		//PowerUp Collision
		if (coll.gameObject.tag == "PowerUp") {
			//Set the color back to blue
			if(pow != powerLevel.none){
				pow_Img[(int)pow-1].color = Color.blue;
			}
			//Increase Pow, and set to red
			pow++;
			pow_Img[(int)pow-1].color = Color.red;
			Destroy (coll.gameObject);
		}
	}

	void resetPowers(){
    // I modified this for() loop to change automatically to pow_Img.Length,
    // so we dont need to change the value manually.
		for(int i = 0; i < pow_Img.Length; i++){
			powers[i] = 0;
			pow_Img[i].color = Color.blue;
			pow_Lbl[i].enabled = true;
		}
	}
}
