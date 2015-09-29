using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Enumerator for our two-tier PowerUp System
enum powerLevel{none, first, second, third, fourth, fifth, sixth};

public class PlayerController : MonoBehaviour {

    // explosion
    public GameObject playerExplosion;

    //audioSources
    public AudioSource shotFX, laserFX, powerUPFX, optionFX;
    public AudioSource bgMusic;

    // invincibility toggle
    public bool invincible = false;

    // custom level shield toggle
    public bool custom_shield = false;
    GameObject customShield;            // links to the halo that color-codes the ship
    
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
	public Vector3 missilespawn = new Vector3(0f, 0f, 0f);
	public float missile_reload;
	float missile_currReload; 

	//Option Mechanics
	public GameObject optionPrefab;

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

    void Start()
    {
        //Initiazlize variables
        shipRigid = this.gameObject.GetComponent<Rigidbody>();
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;
        anim = this.GetComponent<Animator>();

        // the custom shield is present but dismissed to remove halo
        //THIS NEEDS TO BE CHANGED...
        //if(Application.loadedLevelName != "Scene_0"){
        Transform customShieldTrans = transform.FindChild("Shields");
        customShield = customShieldTrans.gameObject;
        //}

        //Set the text for player health
        healthText = GameObject.Find("Health").GetComponent<Text>();
        healthText.text = health.ToString();
        invincibleToggle = GameObject.Find("Invincible").GetComponent<Text>();
        invincibleToggle.text = "";

        bgMusic.Play((ulong)0.0); ;

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
		

		//Player movement, includes screen edge bounding
		if(Input.GetKey(KeyCode.UpArrow) && (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y/8 < (camH/2 + camY))){
			speed.y += maxSpeed.y + 2*powers[0];
		}
		if(Input.GetKey(KeyCode.DownArrow) && (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y/8 > (-camH/2 + camY + 2.5f))){
			speed.y -= maxSpeed.y + 2*powers[0];
		}		
		if(Input.GetKey(KeyCode.LeftArrow) && (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/7 > (-camW/2 + camX))){
			speed.x -= maxSpeed.x + 2*powers[0];
		}		
		if(Input.GetKey(KeyCode.RightArrow) && (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/7 < (camW/2 + camX))){
			speed.x += maxSpeed.x + 2*powers[0];
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

        //
        //  TWO TYPES of shooting protocol follow
        // 
        // Shooting on rapid-button press
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Check for reload and Power Level
            GameObject bullet = null;
            if (powers[1] == 1 && missile_currReload <= 0)
            {
                missile_currReload = missile_reload;
                GameObject missile = Instantiate(missilePrefab) as GameObject;
                missile.GetComponent<Rigidbody>().transform.position = this.transform.position;
            }

            if (powers[3] == 0)
            {
                if (powers[2] == 1)
                {
                    GameObject doubleshot = Instantiate(doubleshotPrefab) as GameObject;
                    doubleshot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
                }
                //Create bullet and move it to the player position
                GameObject shot = Instantiate(shotPrefab) as GameObject;
                shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
                //AudioSource shot = this.GetComponent<AudioSource>();
                bullet = shotPrefab;
                shotFX.Play((ulong) 0.0);
            }
            if (powers[3] == 1)
            {
                GameObject shot = Instantiate(laserPrefab) as GameObject;
                shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
                bullet = laserPrefab;
                laserFX.Play((ulong)0.0);
            }
            GameObject[] options = GameObject.FindGameObjectsWithTag("Option");
            for (int i = 0; i < options.Length; i++)
            {
                options[i].GetComponent<OptionBehaviour>().Fire(bullet, shotspawn);
            }
        }

        // Shooting on button hold
        if (Input.GetKey (KeyCode.A) && currReload <= 0) {
			//Check for reload and Power Level
			GameObject bullet = null;

			if(powers[1] == 1 && missile_currReload <= 0){
				missile_currReload = missile_reload;
				GameObject missile = Instantiate(missilePrefab) as GameObject;
                missile.GetComponent<Rigidbody>().transform.position = this.transform.position;
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
				bullet = shotPrefab;
                shotFX.Play((ulong)0.0);
            }
			if(powers[3] == 1){
				GameObject shot = Instantiate(laserPrefab) as GameObject;
				shot.GetComponent<Rigidbody>().MovePosition(this.transform.position + shotspawn);
				bullet = laserPrefab;
                laserFX.Play((ulong)0.0);
            }
			GameObject[] options = GameObject.FindGameObjectsWithTag("Option");
			for(int i = 0; i < options.Length; i++){
				options[i].GetComponent<OptionBehaviour>().Fire(bullet,shotspawn);
			}
		}

		//Using PowerUp
		if(Input.GetKey(KeyCode.S) && pow != powerLevel.none)
        {
            // play SFX
            optionFX.Play((ulong)0.0);

            if (pow == powerLevel.first){
				powers[(int)pow-1]++;
				pow_Img[(int)pow-1].color = Color.blue;
				//pow_Lbl[(int)pow-1].enabled = false;
				pow = powerLevel.none;
			}	else if(pow == powerLevel.fifth && powers[4] < 2) {
                GameObject option = Instantiate(optionPrefab) as GameObject;
				option.GetComponent<Rigidbody>().position = this.transform.position;
				powers[(int)pow-1]++;
				pow_Img[(int)pow-1].color = Color.blue;
				if(powers[4] == 2){
					pow_Lbl[4].enabled = false;
				}
				pow = powerLevel.none;
			}	
				else if(powers[(int)pow-1] == 0){
				powers[(int)pow-1]++;
				pow_Img[(int)pow-1].color = Color.blue;
				pow_Lbl[(int)pow-1].enabled = false;
				pow = powerLevel.none;
			} 
		}
	}
    

	public void Crash(){
		if (!invincible)
		{
			if (powers[5] == 0)
			{
                // "health" in this sense is player lives
				health--;
				healthText.text = health.ToString();
				
                // instantiate explosion
                GameObject ex = Instantiate(playerExplosion) as GameObject;
                Vector3 exLoc = Vector3.zero;
                exLoc.x = this.transform.position.x;
                exLoc.y = this.transform.position.y;
                exLoc.z = 5f;
                ex.transform.position = exLoc;

                // disable the player ship
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                GetComponentInChildren<Player2DColl>().disableCollider();
                if (custom_shield) { GetComponentInChildren<ShieldToggling>().clearShieldsOnDeath(); }

                // stop music
                bgMusic.Stop();

                // reset the ship and the scheduler
                Invoke("ResetShip", 4f);
            }
			else
			{
				powers[5] = 0;
				pow_Img[5].color = Color.blue;
				pow_Lbl[5].enabled = true;
			}
		}
	}
	
	//Collision detection
	void  OnTriggerEnter(Collider coll){
        //Enemy Collision
		if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyShot")
        {
			Crash();
        }

        //PowerUp Collision
        // note: this checks for the "big power up" in lvl 2 first
        if (coll.gameObject.tag == "PowerUp")
        {
            bool isBigPowerUp = coll.GetComponent<PowerUpMovement>().bigPowerUp;
            if (!isBigPowerUp)
            {
                if (pow != powerLevel.sixth)
                {
                    //Set the color back to blue
                    if (pow != powerLevel.none)
                    {
                        pow_Img[(int)pow - 1].color = Color.blue;
                    }
                    //Increase Pow, and set to red
                    pow++;
                    pow_Img[(int)pow - 1].color = Color.red;
                    Destroy(coll.gameObject);
                    powerUPFX.Play((ulong)0.0);
                }
            } else {
                custom_shield = isBigPowerUp;
                ShieldToggling togg = customShield.GetComponent<ShieldToggling>();
                togg.StartToggle();

                Destroy(coll.gameObject);
            }
		}

        // collision with shield
        if (coll.gameObject.tag == "Laser")
        {
            bool shipBlue = (customShield.GetComponent<ShieldToggling>().isBlue);
            bool laserBlue = (coll.gameObject.GetComponent<LaserWallBehaviour>().isBlue);
            if (shipBlue != laserBlue) {
                Crash();
            } else if (shipBlue == laserBlue) {
                
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

    void ResetShip()
    {
        //Reset the game if player health drops to 0
        if (health <= 0)    {Application.LoadLevel("Scene_0");}

        //reset the shield if custom level
        if (custom_shield) { GetComponentInChildren<ShieldToggling>().StartToggle(); }
        // reset the board - functionality is in scheduler
        // this replaces Checkpoint();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Scheduler>().ResetBoard();

        //Reset powerups
        this.resetPowers();
        pow = powerLevel.none;

        //restart music
        bgMusic.Play((ulong)0.0);

        // enable the player ship
        this.transform.position = new Vector3(-6f, 0f, 0f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        GetComponentInChildren<Player2DColl>().enableCollider();
    }
}

