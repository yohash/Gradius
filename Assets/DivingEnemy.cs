using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DivingEnemy : BasicEnemyBehaviour {
	
	//Setting up oscillations
	public float speed = 4f;
	//private float wav_Time;
	//private float y0;
	private motionState state = motionState.first;
	float camH, camW;
	
	Rigidbody enemyRigid;
	
	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		//wav_Time = Time.time;
		enemyRigid = this.GetComponent<Rigidbody> ();
		//y0 = this.enemyRigid.position.y;
		this.enemyRigid.velocity = new Vector3(-speed, 0f, 0f);
		
		base.score = GameObject.Find ("Score").GetComponent<Text> ();
	}
	
	public override void Move(){
			if (this.transform.position.y <= GameObject.Find("Player").GetComponent<Rigidbody>().position.y + 0.5f && this.transform.position.y >= GameObject.Find("Player").GetComponent<Rigidbody>().position.y - 0.5f){
				this.enemyRigid.velocity = new Vector3(-speed*3f, 0f, 0f);
			} else if (this.transform.position.y <= GameObject.Find("Player").GetComponent<Rigidbody>().position.y){
				this.enemyRigid.velocity = new Vector3(-speed, speed, 0f);
			} else if (this.transform.position.y >= GameObject.Find("Player").GetComponent<Rigidbody>().position.y){
				this.enemyRigid.velocity = new Vector3(-speed, -speed, 0f);
			}
	}
	
	// Update is called once per fra
}

