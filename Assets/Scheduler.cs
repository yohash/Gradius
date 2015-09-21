using UnityEngine;
using System.Collections;

enum enemyID{fan, div, osc, oscP};

public class Scheduler : MonoBehaviour {

	// Catalog of spawnable game objects
	public GameObject[] 		Catalog;	// enemy types to spawn
	public float[] 				relTime;	// relative time from last enemy
	public Vector3[]			enemyLoc;	// enemy location
	public int[]			ID;

	float camH, camW;		// camera data

	float startTime; 		// time game started for event referencing
	int index;			// schedule index

	void Start () {
		relTime = new float[18];
		enemyLoc = new Vector3[18];
		ID = new int[18];

		relTime[0] = 2f;			enemyLoc [0] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [0] = (int)enemyID.fan;
		relTime[1] = 2.25f;			enemyLoc [1] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [1] = (int)enemyID.fan;	
		relTime[2] = 2.25f;			enemyLoc [2] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [2] = (int)enemyID.fan;		
		relTime[3] = 2.25f;			enemyLoc [3] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [3] = (int)enemyID.fan;	
		relTime[4] = 2.25f;			enemyLoc [4] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [4] = (int)enemyID.fan;

		relTime[5] = 0.75f;			enemyLoc [5] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [5] = (int)enemyID.div;
		relTime[6] = 1.5f;			enemyLoc [6] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [6] = (int)enemyID.div;
		relTime[7] = 1.5f;			enemyLoc [7] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [7] = (int)enemyID.div;
		relTime[8] = 1.5f;			enemyLoc [8] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [8] = (int)enemyID.div;

		relTime[9] = 2f;			enemyLoc [9] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [9] = (int)enemyID.fan;
		relTime[10] = 2.25f;		enemyLoc [10] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [10] = (int)enemyID.fan;	
		relTime[11] = 2.25f;		enemyLoc [11] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [11] = (int)enemyID.fan;

		relTime[12] = 0.75f;		enemyLoc [12] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [12] = (int)enemyID.div;
		relTime[13] = 1.5f;			enemyLoc [13] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [13] = (int)enemyID.div;
		relTime[14] = 1.5f;			enemyLoc [14] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [14] = (int)enemyID.div;

		relTime[15] = 1.5f;			enemyLoc [15] = new Vector3 (camW/2-1.0f, -3f, 0f);			ID [15] = (int)enemyID.osc;
		relTime[16] = 1.5f;			enemyLoc [16] = new Vector3 (camW/2-1.0f, -7f, 0f);			ID [16] = (int)enemyID.oscP;	
		relTime[17] = 1.5f;			enemyLoc [17] = new Vector3 (camW/2-1.0f, -3f, 0f);			ID [17] = (int)enemyID.osc;	

		index = 0; 		// initiate the index
		startTime = Time.time;

		Invoke ("Spawn", relTime[index]);
	}

	void Awake(){
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
	}

	public void Spawn() {

		GameObject enemy = Instantiate(Catalog[ID[index]]) as GameObject;

		Vector3 new_Pos = Vector3.zero;

		new_Pos.x = enemyLoc[index].x;
		new_Pos.y = enemyLoc[index].y;

		enemy.transform.position = new_Pos;

		if (index < relTime.Length) {
			index++;
			Invoke ("Spawn", relTime [index]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
