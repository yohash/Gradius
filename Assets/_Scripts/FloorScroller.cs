using UnityEngine;
using System.Collections;

public class FloorScroller : MonoBehaviour
{
    public GameObject map;

	public int animated = 0;

    float speed = 3f;    // speed of the map moving  

	float camW, camH;

    // Use this for initialization
    void Start()
    {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
    }

    // Update is called once per framess
    void FixedUpdate() {
        // initially, the platform floats out and pauses once it's on screen
        if (map.transform.position.x > -camW/2f) {
            map.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
		} else if(animated == 0 && map.transform.position.x <= (-camW/2f)) {
			animated = 1;
		} else if(animated == 1 && map.transform.position.x <= (-camW/2f)-3.3f) {
			map.transform.position = new Vector3(-camW/2, map.transform.position.y, 0);
		} else if (animated == 1) {
			Vector3 point = new Vector3((-camW/2f)-6.6f, map.transform.position.y, 0);
			float step = speed*Time.deltaTime;
			map.transform.position = Vector3.MoveTowards(map.transform.position, point, step);
			print ("floor animating: "+map.transform.position.x+", move spd: "+step+", move to: "+point);
		}
	}
}
