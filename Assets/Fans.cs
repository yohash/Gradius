using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum motionState{first, second, third};

public class Fans : BasicEnemyBehaviour {
	
	//Setting up oscillations
	public float speed = 10f;
	private float wav_Time;
	private float y0;
	private motionState state = motionState.first;
	float camH, camW;

	Rigidbody enemyRigid;
	
	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		wav_Time = Time.time;
		enemyRigid = this.GetComponent<Rigidbody> ();
		y0 = this.enemyRigid.position.y;
		this.enemyRigid.velocity = new Vector3(-speed, 0f, 0f);
		
		base.score = GameObject.Find ("Score").GetComponent<Text> ();

		//InvokeRepeating ("Fire", 1f, 2f); //<---- test fire function
	}
	
	public override void Move(){
		if (this.transform.position.x <= -camW/7f && state == motionState.first){
			state = motionState.second;
			this.enemyRigid.velocity = new Vector3(speed, 0f, 0f);
		}
		if (state == motionState.second){
			if (this.transform.position.y <= GameObject.Find("Player").GetComponent<Rigidbody>().position.y + 0.5f && this.transform.position.y >= GameObject.Find("Player").GetComponent<Rigidbody>().position.y - 0.5f){
				state = motionState.third;
				this.enemyRigid.velocity = new Vector3(speed*1.5f, 0f, 0f);
			} else if (this.transform.position.y <= GameObject.Find("Player").GetComponent<Rigidbody>().position.y){
				this.enemyRigid.velocity = new Vector3(1.5f*speed, 1.5f*speed*0.5f, 0f);
			} else if (this.transform.position.y >= GameObject.Find("Player").GetComponent<Rigidbody>().position.y){
				this.enemyRigid.velocity = new Vector3(speed, -1.5f*speed*0.5f, 0f);
			}
		}
	}
	
	// Update is called once per fra
}

