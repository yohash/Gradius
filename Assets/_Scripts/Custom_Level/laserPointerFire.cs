using UnityEngine;
using System.Collections;

public class laserPointerFire : MonoBehaviour {

    public GameObject laserBlue, laserRed;       // the laser  
    public GameObject haloBlue, haloRed;       // the laser    

    public GameObject bossLaser;            // the parent object of the lasers
                                            // THIS is what actually rotates
    public float laserRate = 6f;            // how fast do we shoot the lasers?
    public float laserTimer;

    public float rotationDegrees = 60f;    // the total angle outward we want to encompass
                                           //      centered about 
    float rotationSpeed;            // rotation speed
    float currentRotation;          // tracking

    bool firing = true;

    // with the current boss-laser arrangement, 
    //    90 degrees is: laser sticks straight out to the left (anchor is far right)
    //                  - 0 degrees is straight up
    //                  - 180 degrees is straight down

    // Use this for initialization
    void Start () {
        // the shooting animation is 5 seconds long
        //      first 3 sec: charging up
        //      last 2 sec: discharging
        rotationSpeed = rotationDegrees / 2f;
        currentRotation = -rotationDegrees / 2f;
        bossLaser.gameObject.transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // lasers are on a 5 second cycle with current animation
        if ((Time.time - laserTimer) > laserRate && !firing)
        {
            if (Random.Range(0f, 1f) > 0.5)
            {
                fireRed();
                firing = true;
                laserTimer = Time.time;
            }
            else
            {
                fireBlue();
                firing = true;
                laserTimer = Time.time;
            }
        }

        // is the lasers are active, move them
        if (laserBlue.gameObject.activeSelf || laserRed.gameObject.activeSelf)
        {
            currentRotation += (Time.deltaTime * rotationSpeed);
        } else
        {
            currentRotation = -rotationDegrees / 2f;
        }
        bossLaser.gameObject.transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
    }

    public void initiateFire() {
        laserTimer = Time.time;
        firing = false;
    }

    void fireRed()
    {
        haloRed.gameObject.SetActive(true);
        Invoke("initiateRedLaser", 3f);
        Invoke("disableRedHalo", 5f);
        Invoke("disableRedLaser", 5f);
    }

    void fireBlue()
    {
        haloBlue.gameObject.SetActive(true);
        Invoke("initiateBlueLaser", 3f);
        Invoke("disableBlueHalo", 5f);
        Invoke("disableBlueLaser", 5f);

    }

    void initiateBlueLaser()
    {
        laserBlue.gameObject.SetActive(true);
    }
    void initiateRedLaser()
    {
        laserRed.gameObject.SetActive(true);
    }

    void disableBlueHalo()
    {
        haloBlue.gameObject.SetActive(false);
    }
    void disableRedHalo()
    {
        haloRed.gameObject.SetActive(false);
    }

    void disableRedLaser()
    {
        laserRed.gameObject.SetActive(false);
        firing = false;
    }
    void disableBlueLaser()
    {
        laserBlue.gameObject.SetActive(false);
        firing = false;
    }

}
