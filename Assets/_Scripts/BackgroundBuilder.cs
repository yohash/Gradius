using UnityEngine;
using System.Collections;

public class BackgroundBuilder : MonoBehaviour {

	public GameObject background;
	float camH, camW;
	public int count;
	
	void Start () {
		//Variable Initialization
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
        camH /= 2f;
		camW /= 1.5f;
        
		//Instatiate the Stars
		GameObject back;
		for (int i =0; i <count; ++i) {
			back = Instantiate(background) as GameObject;
			back.transform.position = new Vector3(camW*Random.Range (-1f,1f), camH*Random.Range (-1f,1f), 10);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
