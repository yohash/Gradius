using UnityEngine;
using System.Collections;

enum volc{moving, shooting, leaving}

public class VolcanoScript : BasicEnemyBehaviour
{
    // turret speed MUST match the speed of the floor
    public float speed = 3f;

    // how long are volcanos scheduled to be here?
    public float dwellTime = 10f;
    public float startTime;

    GameObject selfGO;
    private volc volcStatus = volc.moving;

    // Use this for initialization
    void Start () {
        selfGO = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (volcStatus == volc.moving && selfGO.transform.position.y > 0)
        {
            this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        } else if (volcStatus == volc.moving && selfGO.transform.position.y <= 0)
        {
            volcStatus = volc.shooting;
            startTime = Time.time;
        } else if (volcStatus == volc.shooting && (Time.time - startTime) < dwellTime) {
            foreach (Transform child in selfGO.transform) {
                print(child);
            }            
        }        
    }
}
