using UnityEngine;
using System.Collections;

enum enemyID{fans, osc};

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
		relTime = new float[12];
		enemyLoc = new Vector3[12];
		ID = new int[12];

		relTime[0] = 1f;		enemyLoc [0] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);		ID [0] = (int)enemyID.fans;
		relTime[1] = 0.5f;		enemyLoc [1] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);		ID [1] = (int)enemyID.fans;
		relTime[2] = 0.5f;		enemyLoc [2] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);		ID [2] = (int)enemyID.fans;
		relTime[3] = 0.5f;		enemyLoc [3] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);		ID [3] = (int)enemyID.fans;
		relTime[4] = 4f;		enemyLoc [4] = new Vector3 (camW/2-1.0f, camH/3f, 0f);		ID [4] = (int)enemyID.fans;	
		relTime[5] = 0.5f;		enemyLoc [5] = new Vector3 (camW/2-1.0f, camH/3f, 0f);		ID [5] = (int)enemyID.fans;
		relTime[6] = 0.5f;		enemyLoc [6] = new Vector3 (camW/2-1.0f, camH/3f, 0f);		ID [6] = (int)enemyID.fans;
		relTime[7] = 0.5f;		enemyLoc [7] = new Vector3 (camW/2-1.0f, camH/3f, 0f);		ID [7] = (int)enemyID.fans;
		relTime[8] = 2f;		enemyLoc [8] = new Vector3 (camW/2-1.0f, -camH/3f+1f, 0f);	ID [8] = (int)enemyID.osc;
		relTime[9] = 0f;		enemyLoc [9] = new Vector3 (camW/2-1.0f, -camH/3f-1f, 0f);	ID [9] = (int)enemyID.osc;
		relTime[10] = 2f;		enemyLoc [10] = new Vector3 (camW/2-1.0f, camH/3f+1f, 0f);	ID [10] = (int)enemyID.osc;
		relTime[11] = 0f;		enemyLoc [11] = new Vector3 (camW/2-1.0f, camH/3f-1f, 0f);	ID [11] = (int)enemyID.osc;

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

		if (index <= relTime.Length) {
			index++;
			Invoke ("Spawn", relTime [index]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
