using UnityEngine;
using System.Collections;

enum enemyID{fan, osc, div};

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
		relTime = new float[62];
		enemyLoc = new Vector3[62];
		ID = new int[62];

		relTime[0] = 2f;		enemyLoc [0] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [0] = (int)enemyID.fan;
		relTime[1] = 0.25f;		enemyLoc [1] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [1] = (int)enemyID.fan;
		relTime[2] = 0.25f;		enemyLoc [2] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [2] = (int)enemyID.fan;
		relTime[3] = 0.25f;		enemyLoc [3] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [3] = (int)enemyID.fan;

		relTime[4] = 1.5f;		enemyLoc [4] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [4] = (int)enemyID.fan;	
		relTime[5] = 0.25f;		enemyLoc [5] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [5] = (int)enemyID.fan;
		relTime[6] = 0.25f;		enemyLoc [6] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [6] = (int)enemyID.fan;
		relTime[7] = 0.25f;		enemyLoc [7] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [7] = (int)enemyID.fan;

		relTime[8] = 1.5f;		enemyLoc [8] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [8] = (int)enemyID.fan;
		relTime[9] = 0.25f;		enemyLoc [9] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [9] = (int)enemyID.fan;
		relTime[10] = 0.25f;	enemyLoc [10] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [10] = (int)enemyID.fan;
		relTime[11] = 0.25f;	enemyLoc [11] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [11] = (int)enemyID.fan;
		
		relTime[12] = 1.5f;		enemyLoc [12] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [12] = (int)enemyID.fan;	
		relTime[13] = 0.25f;	enemyLoc [13] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [13] = (int)enemyID.fan;
		relTime[14] = 0.25f;	enemyLoc [14] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [14] = (int)enemyID.fan;
		relTime[15] = 0.25f;	enemyLoc [15] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [15] = (int)enemyID.fan;

		relTime[16] = 1.5f;		enemyLoc [16] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [16] = (int)enemyID.fan;
		relTime[17] = 0.25f;	enemyLoc [17] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [17] = (int)enemyID.fan;
		relTime[18] = 0.25f;	enemyLoc [18] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [18] = (int)enemyID.fan;
		relTime[19] = 0.25f;	enemyLoc [19] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [19] = (int)enemyID.fan;

		relTime[20] = 0f;		enemyLoc [20] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [20] = (int)enemyID.div;
		relTime[21] = 0f;		enemyLoc [21] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [21] = (int)enemyID.div;
		relTime[22] = 0f;		enemyLoc [22] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [22] = (int)enemyID.div;
		relTime[23] = 1.5f;		enemyLoc [23] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [23] = (int)enemyID.div;
		relTime[24] = 0f;		enemyLoc [24] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [24] = (int)enemyID.div;
		relTime[25] = 0f;		enemyLoc [25] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [25] = (int)enemyID.div;
		relTime[26] = 1.5f;		enemyLoc [26] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [26] = (int)enemyID.div;
		relTime[27] = 0f;		enemyLoc [27] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [27] = (int)enemyID.div;
		relTime[28] = 0f;		enemyLoc [28] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [28] = (int)enemyID.div;
		relTime[29] = 1.5f;		enemyLoc [29] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [29] = (int)enemyID.div;
		relTime[30] = 0f;		enemyLoc [30] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [30] = (int)enemyID.div;
		relTime[31] = 0f;		enemyLoc [31] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [31] = (int)enemyID.div;

		relTime[32] = 4f;		enemyLoc [32] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [32] = (int)enemyID.fan;
		relTime[33] = 0.25f;	enemyLoc [33] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [33] = (int)enemyID.fan;
		relTime[34] = 0.25f;	enemyLoc [34] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [34] = (int)enemyID.fan;
		relTime[35] = 0.25f;	enemyLoc [35] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [35] = (int)enemyID.fan;
		
		relTime[36] = 1.5f;		enemyLoc [36] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [36] = (int)enemyID.fan;	
		relTime[37] = 0.25f;	enemyLoc [37] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [37] = (int)enemyID.fan;
		relTime[38] = 0.25f;	enemyLoc [38] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [38] = (int)enemyID.fan;
		relTime[39] = 0.25f;	enemyLoc [39] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [39] = (int)enemyID.fan;
		
		relTime[40] = 1.5f;		enemyLoc [40] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [40] = (int)enemyID.fan;
		relTime[41] = 0.25f;	enemyLoc [41] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [41] = (int)enemyID.fan;
		relTime[42] = 0.25f;	enemyLoc [42] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [42] = (int)enemyID.fan;
		relTime[43] = 0.25f;	enemyLoc [43] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [43] = (int)enemyID.fan;

		relTime[44] = 0f;		enemyLoc [44] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [44] = (int)enemyID.div;
		relTime[45] = 0f;		enemyLoc [45] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [45] = (int)enemyID.div;
		relTime[46] = 0f;		enemyLoc [46] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [46] = (int)enemyID.div;
		relTime[47] = 1.5f;		enemyLoc [47] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [47] = (int)enemyID.div;
		relTime[48] = 0f;		enemyLoc [48] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [48] = (int)enemyID.div;
		relTime[49] = 0f;		enemyLoc [49] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [49] = (int)enemyID.div;
		relTime[50] = 1.5f;		enemyLoc [50] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [50] = (int)enemyID.div;
		relTime[51] = 0f;		enemyLoc [51] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [51] = (int)enemyID.div;
		relTime[52] = 0f;		enemyLoc [52] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [52] = (int)enemyID.div;

		relTime[53] = 0.5f;		enemyLoc [53] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [53] = (int)enemyID.osc;	
		relTime[54] = 0f;		enemyLoc [54] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [54] = (int)enemyID.osc;

		relTime[55] = 1.0f;		enemyLoc [55] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [55] = (int)enemyID.div;
		relTime[56] = 0f;		enemyLoc [56] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [56] = (int)enemyID.div;
		relTime[57] = 0f;		enemyLoc [57] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [57] = (int)enemyID.div;


		relTime[58] = 0f;		enemyLoc [58] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [58] = (int)enemyID.osc;
		relTime[59] = 0f;		enemyLoc [59] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [59] = (int)enemyID.osc;
		relTime[60] = 1f;		enemyLoc [60] = new Vector3 (camW/2-1.0f, camH/4f, 0f);		ID [60] = (int)enemyID.osc;
		relTime[61] = 0f;		enemyLoc [61] = new Vector3 (camW/2-1.0f, -camH/4f, 0f);	ID [61] = (int)enemyID.osc;


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
