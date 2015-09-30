using UnityEngine;
using System.Collections;

enum volc{moving, shooting, leaving}

public class VolcanoScript : BasicEnemyBehaviour
{
    // turret speed MUST match the speed of the floor
    public float speed = 3f;

    // how long are volcanos scheduled to be here?
    public float dwellTime = 18f;
    public float startTime;

    // how quickly does volcano shoot
    public float timeBtwShots = 0.25f;
    public float delayTime;

    // self reference and children reference
    GameObject selfGO;
    GameObject[] volcs;
    private volc volcStatus = volc.moving;

    // Use this for initialization
    void Start () {
        selfGO = this.gameObject;
        volcs = GameObject.FindGameObjectsWithTag("Volcano"); 
    }
	
	// Update is called once per frame
	void Update () {
        if (volcStatus == volc.moving && selfGO.transform.position.x > -6)
        {
            this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        }
        else if (volcStatus == volc.moving && selfGO.transform.position.y <= 0)
        {
			GameObject.FindGameObjectWithTag("FlrCeil").GetComponent<FloorScroller>().animated = 2;
			volcStatus = volc.shooting;
			startTime = Time.time;
            delayTime = Time.time;
        }
        else if (volcStatus == volc.shooting && (Time.time - startTime) < dwellTime)
        {
            if (Time.time - delayTime > timeBtwShots)
            {
                foreach (GameObject v in volcs)
                {
                    if (v.tag == "Volcano") {
                        MountainBehavior vFire = v.GetComponent<MountainBehavior>();
                        vFire.callFire();
                    }                    
                }
                delayTime = Time.time;
            }
        }
        else if (volcStatus == volc.shooting && (Time.time - startTime) > dwellTime)
        {
            volcStatus = volc.leaving;
		}
        else if (volcStatus == volc.leaving)
        {
			GameObject.FindGameObjectWithTag("FlrCeil").transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
            this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        }
    }
}
