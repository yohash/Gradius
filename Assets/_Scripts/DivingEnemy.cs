using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DivingEnemy : BasicEnemyBehaviour {

	//Animation
	Animator anim;

    //Setting up oscillations
    public bool isShooter;
	public float speed = 5f;
    //float camH, camW; //<---- silenced due to BasicEnemyBehavior computing same values

    private motionState state = motionState.first;

    Rigidbody enemyRigid;
    Vector3 playerPos;

    // Use this for initialization
    void Start () {
		//Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		//camH = cam.orthographicSize * 2f;
		//camW = camH * cam.aspect;
		//wav_Time = Time.time;

		anim = this.GetComponent<Animator>();
		enemyRigid = this.GetComponent<Rigidbody> ();

		this.enemyRigid.velocity = new Vector3(-speed, 0f, 0f);
		
		base.score = GameObject.Find ("Score").GetComponent<Text> ();
        
        if (isShooter) { Invoke("Fire", 4f); }
	}
	
	public override void Move()
    {
        playerPos = GameObject.Find("Player").GetComponent<Rigidbody>().position;

        if (this.transform.position.y <= playerPos.y + 0.5f && this.transform.position.y >= playerPos.y - 0.5f){
				this.enemyRigid.velocity = new Vector3(-speed*2.25f, 0f, 0f);
				anim.SetFloat("speed", this.enemyRigid.velocity.y);
			} else if (this.transform.position.y <= playerPos.y){
				this.enemyRigid.velocity = new Vector3(-2f, 1f, 0f).normalized * speed;
				anim.SetFloat("speed", this.enemyRigid.velocity.y);
			} else if (this.transform.position.y >= playerPos.y){
				this.enemyRigid.velocity = new Vector3(-2f, -1f, 0f).normalized * speed;
				anim.SetFloat("speed", this.enemyRigid.velocity.y);
			}

	}
	
	// Update is called once per frame

}

