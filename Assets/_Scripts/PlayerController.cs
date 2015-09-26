using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Enumerator for our two-tier PowerUp System
enum powerLevel{none, first, second, third, fourth, fithh, sixth};

public class PlayerController : MonoBehaviour {

    // invincibility toggle
    public bool invincible = false;

	//Animation
	Animator anim;

	//UI elements
	public int health = 3;
	Text healthText;
    Text invincibleToggle;

	//Player Control
	public Vector2 maxSpeed;
	Rigidbody shipRigid;
	float camW, camH;

	//Shooting Mechanics 
	public GameObject shotPrefab;
	public Vector3 shotspawn = new Vector3(1.5f, 0f, 0f);
	public float reload;
	float currReload;

	//Missile Mechanics
	public GameObject missilePrefab;
	public Vector3 missilespawn = new Vector3(0.5f, 0.5f, 0f);
	public float missile_reload;
	float missile_currReload; 

	//Shield Mechanics


	//Double Shot Mechanics
	public GameObject doubleshotPrefab;

	//Laser Mechanics
	public GameObject laserPrefab;


	//PowerUps
	powerLevel pow = powerLevel.none;
	int[] powers = {1,0,0,0,0,0};
	public Image[] pow_Img = new Image[6];
	public Text[] pow_Lbl = new Text[6];
	
	void Start () {
		//Initiazlize variables
		shipRigid = this.gameObject.GetComponent<Rigidbody> ();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		anim = this.GetComponent<Animator>();

        //Set the text for player health
        healthText = GameObject.Find("Health").GetComponent<Text>();
        healthText.text = health.ToString();
        invincibleToggle = GameObject.Find("Invincible").GetComponent<Text>();
        invincibleToggle.text = "";

        //Add the power images/labels to the array
        //		pow_Img[0] = GameObject.Find("Power_Speed").GetComponent<Image> ();
        //        pow_Img[1] = GameObject.Find("Power_Missile").GetComponent<Image>();
        //        pow_Img[2] = GameObject.Find("Power_Double").GetComponent<Image>();
        //        pow_Img[3] = GameObject.Find("Power_Laser").GetComponent<Image>();
        //        pow_Img[4] = GameObject.Find("Power_Option").GetComponent<Image>();
        //		pow_Img[5] = GameObject.Find("Power_Shield").GetComponent<Image>();
        //		pow_Lbl[0] = GameObject.Find("Power_Speed_Label").GetComponent<Text>();
        //        pow_Lbl[1] = GameObject.Find("Power_Missile_Label").GetComponent<Text>();
        //        pow_Lbl[2] = GameObject.Find("Power_Double_Label").GetComponent<Text>();
        //        pow_Lbl[3] = GameObject.Find("Power_Laser_Label").GetComponent<Text>();
        //        pow_Lbl[4] = GameObject.Find("Power_Option_Label").GetComponent<Text>();
        //		pow_Lbl[5] = GameObject.Find("Power_Shield_Label").GetComponent<Text>();


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

		if (missile_currReload < Time.deltaTime) {
			missile_currReload = 0f;
		} else {
			missile_currReload -= Time.deltaTime;
		}
		
		//Reset the game if player health drops to 0
		if (health <= 0) {
			Application.LoadLevel("Scene_0");
		}

		//Player movement, includes screen edge bounding
		if(Input.GetKey(KeyCode.UpArrow) && (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y/8 < (camH/2 + camY))){
			speed.y += maxSpeed.y*powers[0];
		}
		if(Input.GetKey(KeyCode.DownArrow) && (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y/8 > (-camH/2 + camY))){
			speed.y -= maxSpeed.y*powers[0];
		}		
		if(Input.GetKey(KeyCode.LeftArrow) && (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/7 > (-camW/2 + camX))){
			speed.x -= maxSpeed.x*powers[0];
		}		
		if(Input.GetKey(KeyCode.RightArrow) && (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/7 < (camW/2 + camX))){
			speed.x += maxSpeed.x*powers[0];
        }
        // toggle invincibility
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (invincible)
            {
                invincible = false;
                invincibleToggle.text = "";
            } else {
                invincible = true;
                invincibleToggle.text = "INVINCIBLE";
            }
        }

        // if ship is off screen (-x) add an offset to keep us on screen
        float offset = this.transform.position.x - (camX - camW / 2);
        if(offset < 0) {
            shipRigid.position -= new Vector3(offset, 0, 0);
        } else {
            shipRigid.velocity = speed;
			anim.SetFloat("speed", shipRigid.velocity.y);
        }

		//Shooting
		if (Input.GetKey (KeyCode.A) && currReload <= 0) {
			//Check for reload and Power Level
			if(powers[1] == 1 && missile_currReload <= 0){
				missile_currReload = missile_reload;
				GameObject missile = Instantiate(missilePrefab) as GameObject;
				missile.GetComponent<Rigidbody>().MovePosition(this.transform.position + missilespawn);
			}
			currReload = reload;

			if(powers[3] == 0){
				if(powers[2] == 1){
					GameObject doubleshot = Instantiate(doubleshotPrefab) as GameObject;
					doubleshot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
				}
				//Create bullet and move it to the player position
				GameObject shot = Instantiate(shotPrefab) as GameObject;
				shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
			}
			if(powers[3] == 1){
				GameObject shot = Instantiate(laserPrefab) as GameObject;
				shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
			}
		}

		//Using PowerUp
		if(Input.GetKey(KeyCode.S) && pow != powerLevel.none)
		{
			if (pow == powerLevel.first){
				powers[(int)pow-1]++;
				pow_Img[(int)pow-1].color = Color.blue;
				//pow_Lbl[(int)pow-1].enabled = false;
				pow = powerLevel.none;
			}	else if(powers[(int)pow-1] == 0){
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
        if (!invincible)
        {
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyShot" || coll.gameObject.tag == "Ground")
            {
                if (powers[5] == 0)
                {
                    health--;
                    healthText.text = health.ToString();

                    //Reset powerups
                    resetPowers();
                    pow = powerLevel.none;
                }
                else
                {
                    powers[5] = 0;
                    pow_Img[5].color = Color.blue;
                    pow_Lbl[5].enabled = true;
                }
            }
        }
		//PowerUp Collision
		if (coll.gameObject.tag == "PowerUp") {
			if(pow != powerLevel.sixth){
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
	}

	void resetPowers(){
    // I modified this for() loop to change automatically to pow_Img.Length,
    // so we dont need to change the value manually.
		for(int i = 0; i < pow_Img.Length; i++){
			powers[i] = 0;
			pow_Img[i].color = Color.blue;
			pow_Lbl[i].enabled = true;
		}
		powers[0] = 1;
	}
}
