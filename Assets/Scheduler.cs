using UnityEngine;
using System.Collections;

public class Scheduler : MonoBehaviour {

	// Catalog of spawnable game objects
	public GameObject[] 		Catalog;	// enemy types to spawn
	public float[] 				relTime;	// relative time from last enemy
	public Vector3[]			enemyLoc;	// enemy location

	float camH, camW;		// camera data

	float startTime; 		// time game started for event referencing
	int index;			// schedule index

	void Start () {
		relTime = new float[10];
		enemyLoc = new Vector3[10];

		relTime[0] = 4f;		enemyLoc [0] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);
		relTime[1] = 0.5f;		enemyLoc [1] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);
		relTime[2] = 0.5f;		enemyLoc [2] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);
		relTime[3] = 0.5f;		enemyLoc [3] = new Vector3 (camW/2-1.0f, -camH/3f, 0f);
		relTime[4] = 4f;		enemyLoc [4] = new Vector3 (camW/2-1.0f, camH/3f, 0f);
		relTime[5] = 0.5f;		enemyLoc [5] = new Vector3 (camW/2-1.0f, camH/3f, 0f);
		relTime[6] = 0.5f;		enemyLoc [6] = new Vector3 (camW/2-1.0f, camH/3f, 0f);
		relTime[7] = 0.5f;		enemyLoc [7] = new Vector3 (camW/2-1.0f, camH/3f, 0f);
		print (relTime[0]);

		index = 0; 		// initiate the index
		startTime = Time.time;
		
		print (relTime[0]);
		Invoke ("Spawn", relTime[index]);
	}

	void Awake(){
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		print (camW);
		print (camH);
	}

	public void Spawn() {
		GameObject enemy = Instantiate(Catalog[0]) as GameObject;

		Vector3 new_Pos = Vector3.zero;

		new_Pos.x = enemyLoc[index].x;
		new_Pos.y = enemyLoc[index].y;
		
		print (new_Pos);

		enemy.transform.position = new_Pos;

		index++;
		Invoke ("Spawn", relTime[index]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
