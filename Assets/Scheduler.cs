using UnityEngine;
using System.Collections;

public class Scheduler : MonoBehaviour {

	// Catalog of spawnable game objects
	public GameObject[] Catalog;

	//Rudimentary scheduler, for a constant spawn
	public float spawnPerSecond = 2f;
	public float spawnVariance = 2f;

	float camH, camW;

	void Start () {
	
	}

	void Awake(){
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;

		Invoke ("Spawn", spawnPerSecond);
	}

	public void Spawn(){
		GameObject enemy = Instantiate(Catalog[0]) as GameObject;

		Vector3 rand_Pos = Vector3.zero;

		rand_Pos.x = camW/2;
		rand_Pos.y = Random.Range(-camH/2 + 4f, camH/2 - 4f);

		enemy.transform.position = rand_Pos;

		Invoke ("Spawn", spawnPerSecond);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
