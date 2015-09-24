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
	//float camH, camW; //<---- silenced due to BasicEnemyBehavior computing same values

	Rigidbody enemyRigid;
    Vector3 playerPos;
	
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

		// InvokeRepeating ("Fire", 1f, 2f); //<---- test fire function
	}
	
	public override void Move(){

        playerPos = GameObject.Find("Player").GetComponent<Rigidbody>().position;

        if (this.transform.position.x <= -camW/6f && state == motionState.first){
			state = motionState.second;
			this.enemyRigid.velocity = new Vector3(speed, 0f, 0f);
		}
		if (state == motionState.second){
			if (this.transform.position.y <= playerPos.y + 0.5f && this.transform.position.y >= playerPos.y - 0.5f){
				state = motionState.third;
				this.enemyRigid.velocity = new Vector3(speed*1.5f, 0f, 0f);
			} else if (this.transform.position.y <= playerPos.y){
				this.enemyRigid.velocity = new Vector3(1f, 2f, 0f).normalized * speed;
			} else if (this.transform.position.y >= playerPos.y){
				this.enemyRigid.velocity = new Vector3(1f, -2f, 0f).normalized * speed;
			}
		}
	}
}

