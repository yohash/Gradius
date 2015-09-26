using UnityEngine;
using System.Collections;
using System.IO;

enum enemyID{fan, div, divS, osc, oscP, gnd, tur, turP, silo, hop, hopP, ATST, mount, volc, boss};

public class Scheduler : MonoBehaviour {

	// Catalog of spawnable game objects
	public GameObject[] 		Catalog;	// enemy types to spawn
    float[] 				    relTime;	// relative time from last enemy
	Vector3[]			        enemyLoc;	// enemy location
	int[]			            ID;

	float camH, camW;		// camera data

	float startTime; 		// time game started for event referencing
	int index;			// schedule index
    
	void Start ()
    {   // ***************************************************************
        // ******** REPLACE THIS CODE IF BETTER SOLUTION *****************
        // ***************************************************************
        // this section is because I cannot find a simple way to 
        // compute how many lines are in our scheduler text file, so
        // we read it through once to increment a counter
		StringReader reader = null;

		TextAsset fileCount = Resources.Load<TextAsset>("Level_1");
		reader = new StringReader(fileCount.text);
        //System.IO.StreamReader fileCount = new System.IO.StreamReader("Assets/level_1.txt");
        int lineCount = 0;  // track number of lines in the file
        while(reader.ReadLine()!=null) {
            lineCount++;
        }
        // end the counter
        // ***************************************************************
        // ******** REPLACE THIS CODE IF BETTER SOLUTION *****************
        // ***************************************************************

        //now I know how long the doc is, and how many enemies there are
        relTime = new float[lineCount];
        enemyLoc = new Vector3[lineCount];
        ID = new int[lineCount];

        // pull data for level
        // System.IO.StreamReader file = new System.IO.StreamReader("Assets/level_1.txt");
		TextAsset file = Resources.Load<TextAsset>("Level_1");
		reader = new StringReader(file.text);
        string line;
        string[] splitLines;

        // the files must have hard-coded locations installed
        // for reference, orthographic cam with size = 10:
        //     camH = +- 10
        //     camW = +- 13.4
        lineCount = 0;      // reset this counter
        while ((line = reader.ReadLine()) != null)
        {
            float temp1, temp2, temp3;  // temp variables to put in Vector3
            splitLines = line.Split(' ');
            relTime[lineCount] = float.Parse(splitLines[0]);
            
            // read in the temp variables
            temp1 = float.Parse(splitLines[1]);
            temp2 = float.Parse(splitLines[2]);
            temp3 = float.Parse(splitLines[3]);

            // record the Vector3
            enemyLoc[lineCount] = new Vector3(temp1, temp2, temp3);
            // convert and record the enum
            ID[lineCount] = (int) System.Enum.Parse(typeof(enemyID), splitLines[4]);
            lineCount++;
        }

        // our old data to remove at some point
        //relTime[0] = 2f;			enemyLoc [0] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [0] = (int)enemyID.fan;
        //relTime[1] = 2.25f;			enemyLoc [1] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [1] = (int)enemyID.fan;	
        //relTime[2] = 2.25f;			enemyLoc [2] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [2] = (int)enemyID.fan;		
        //relTime[3] = 2.25f;			enemyLoc [3] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [3] = (int)enemyID.fan;	
        //relTime[4] = 2.25f;			enemyLoc [4] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [4] = (int)enemyID.fan;

        //relTime[5] = 0.75f;			enemyLoc [5] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [5] = (int)enemyID.div;
        //relTime[6] = 1.5f;			enemyLoc [6] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [6] = (int)enemyID.div;
        //relTime[7] = 1.5f;			enemyLoc [7] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [7] = (int)enemyID.div;
        //relTime[8] = 1.5f;			enemyLoc [8] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [8] = (int)enemyID.div;

        //relTime[9] = 2f;			enemyLoc [9] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);		ID [9] = (int)enemyID.fan;
        //relTime[10] = 2.25f;		enemyLoc [10] = new Vector3 (camW/2-1.0f, camH/5f, 0f);		ID [10] = (int)enemyID.fan;	
        //relTime[11] = 2.25f;		enemyLoc [11] = new Vector3 (camW/2-1.0f, -camH/5f, 0f);	ID [11] = (int)enemyID.fan;

        //relTime[12] = 0.75f;		enemyLoc [12] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [12] = (int)enemyID.div;
        //relTime[13] = 1.5f;			enemyLoc [13] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [13] = (int)enemyID.div;
        //relTime[14] = 1.5f;			enemyLoc [14] = new Vector3 (camW/2-1.0f, 0f, 0f);			ID [14] = (int)enemyID.div;

        //relTime[15] = 1.5f;			enemyLoc [15] = new Vector3 (camW/2-1.0f, -3f, 0f);			ID [15] = (int)enemyID.osc;
        //relTime[16] = 1.5f;			enemyLoc [16] = new Vector3 (camW/2-1.0f, -7f, 0f);			ID [16] = (int)enemyID.oscP;	
        //relTime[17] = 1.5f;			enemyLoc [17] = new Vector3 (camW/2-1.0f, -3f, 0f);			ID [17] = (int)enemyID.osc;	

        index = 0; 		// initiate the index
        Invoke ("Spawn", relTime[index]);
    }

	void Awake(){
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
    }

	public void Spawn()
    {
        GameObject enemy = Instantiate(Catalog[ID[index]]) as GameObject;

		Vector3 new_Pos = Vector3.zero;

		new_Pos.x = enemyLoc[index].x;
		new_Pos.y = enemyLoc[index].y;

		enemy.transform.position = new_Pos;

		if (index < relTime.Length-1) {
			index++;
			Invoke ("Spawn", relTime [index]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
