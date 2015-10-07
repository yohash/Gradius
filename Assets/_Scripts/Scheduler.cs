using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

enum enemyID{fan, div, divS, osc, oscP, gnd, tur, turP, silo, hop, hopP, ATST, mount, volc, boss, bigPUP, Blaz_m, Rlaz_m, trees, BIG_mount, SPLT_mount, buzzB, buzzR, boss2,SALR, LALR, SARR, LARR, SALB, LALB, SARB, LARB, DSpin, QSpinS, QSpinA, lvl2mount, lvl2block, lvl2gnd};

public class Scheduler : MonoBehaviour {

	// Catalog of spawnable game objects
	public GameObject[] 		Catalog;	// enemy types to spawn
    float[] 				    relTime;	// relative time from last enemy
	Vector3[]			        enemyLoc;	// enemy location
	int[]			            ID;

    public bool customLevel = false;           // if this is true, load level_2.txt

	bool paused = false;

	float camH, camW;		// camera data

	float startTime; 		// time game started for event referencing
	public int index;			// schedule index
    public int lineCount;
    public int isGround;        // thsi track the location of the ground in the scheduler

	void Start ()
    {   // ***************************************************************
        // ******** REPLACE THIS CODE IF BETTER SOLUTION *****************
        // ***************************************************************
        // this section is because I cannot find a simple way to 
        // compute how many lines are in our scheduler text file, so
        // we read it through once to increment a counter
		StringReader reader = null;
        TextAsset fileCount;

        if (customLevel) { fileCount = Resources.Load<TextAsset>("Level_2"); }
		else { fileCount = Resources.Load<TextAsset>("Level_1"); }
		reader = new StringReader(fileCount.text);
        //System.IO.StreamReader fileCount = new System.IO.StreamReader("Assets/level_1.txt");
        lineCount = 0;  // track number of lines in the file
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
        if (customLevel) { fileCount = Resources.Load<TextAsset>("Level_2"); }
        else { fileCount = Resources.Load<TextAsset>("Level_1"); }
        reader = new StringReader(fileCount.text);
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

            if (ID[lineCount] == 5 || ID[lineCount] == 37) { isGround = lineCount; }            
            lineCount++;
        }
        
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
        new_Pos.z = enemyLoc[index].z;

        enemy.transform.position = new_Pos;

		if (index < relTime.Length-1) {
			index++;
			Invoke ("Spawn", relTime [index]);
		}
	}
	
    // called to reset level position after a player death
    public void ResetBoard() {

        CancelInvoke("Spawn");  // cancel any existing invokes
        
        // eliminate everything on the screen
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] mountains = GameObject.FindGameObjectsWithTag("Mountain");
        GameObject[] eshots = GameObject.FindGameObjectsWithTag("EnemyShot");
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
        GameObject[] volcSpwn = GameObject.FindGameObjectsWithTag("VolcanoSpawner");
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Trees");
		GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
		GameObject[] buddies = GameObject.FindGameObjectsWithTag("Option");

        // determine if player was fighting boss or volcanos
        bool bossFight = false, volcano = false;
        if (volcSpwn.Length == 0 && index == relTime.Length-1) { bossFight = true;}
        else if (volcSpwn.Length == 1) { volcano = true; print("volc"); }

        for (int i = 0; i < enemies.Length; i++)    {Destroy(enemies[i].gameObject);}
        for (int i = 0; i < mountains.Length; i++)  {Destroy(mountains[i].gameObject);}
        for (int i = 0; i < eshots.Length; i++)     {Destroy(eshots[i].gameObject);}
        for (int i = 0; i < lasers.Length; i++)     {Destroy(lasers[i].gameObject); }
        for (int i = 0; i < volcSpwn.Length; i++)   {Destroy(volcSpwn[i].gameObject); }
        for (int i = 0; i < trees.Length; i++)      {Destroy(trees[i].gameObject); }
        for (int i = 0; i < powerups.Length; i++)   { Destroy(powerups[i].gameObject); }
		for (int i = 0; i < buddies.Length; i++)   { Destroy(buddies[i].gameObject); }
        

        // if volcanoes, start them over
        if (volcano) { 
            index--;
            GameObject.FindGameObjectWithTag("FlrCeil").GetComponent<FloorScroller>().animated = 1;
        }
        // if not volcano, re-instantiate the boss fight
        else if (bossFight) {

            GameObject enemy = Instantiate(Catalog[ID[index]]) as GameObject;

            Vector3 new_Pos = Vector3.zero;

            new_Pos.x = enemyLoc[index].x;
            new_Pos.y = enemyLoc[index].y;
            new_Pos.z = enemyLoc[index].z;

            enemy.transform.position = new_Pos;
        }
        //otherwise, push schedule back 8 sec.
        else
        {
            // change index of scheduler
            int itmp = index;
            float currTime = 0f, rewTime = 8f;
            // this will "rewind" the map X seconds, instead of just 
            // rewinding 4 "objects", which sometimes would disassemble barricades
            while (itmp >= 0)
            {
                currTime += relTime[itmp];
                if (currTime > rewTime) { break; }
                if (itmp == 0) { break; }
                itmp--;
            }
            index = itmp;
        }

        // if the new index is < the floor-ceiling sprite, then we delete this object
        if (index < isGround) {
            GameObject terrain = GameObject.FindGameObjectWithTag("FlrCeil");
            if (terrain != null) { Destroy(terrain.gameObject); }
        }

        //if this is the custom level and the player hasnt gotten the powerup
        //just start the whole level over
        if (customLevel && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().custom_shield)
        {
            index = 1;
            GameObject terrain = GameObject.FindGameObjectWithTag("FlrCeil");
            if (terrain != null) { Destroy(terrain.gameObject); }
        }


        // reset from the checkpoint by initiating spawn
        if (!bossFight) { Invoke("Spawn", relTime[index] + 4); }   // reset from the last point
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightShift)){
			if(!paused){
				paused = true;
				Time.timeScale = 0;
			} else {
				paused = false;
				Time.timeScale = 1;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Time.timeScale = 1;
		}

		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Time.timeScale = 2;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Time.timeScale = 4;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Time.timeScale = 8;
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Time.timeScale = 16;
		}
	}

    // only the boss calls this, and only on death
    public void youWin()
        {
        Text helperText;
        helperText = GameObject.Find("Helper_Text").GetComponent<Text>();
        helperText.color = Color.white;
        helperText.text = "Great Job!\nYou've defeated\nthe Boss!";
        
        Invoke("loadTitle", 4f);
    }

    void loadTitle() {
        Application.LoadLevel("Scene_Title");
    }
    
}
