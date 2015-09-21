using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OscillatingEnemy : BasicEnemyBehaviour {

	//Setting up oscillations
	public float wav_Freq = 2;
	public float wav_Len  = 3;
	private float wav_Time;
	private float y0;

	float speed = 4f;
	Rigidbody enemyRigid;

	// Use this for initialization
	void Start () {
		wav_Time = Time.time;
		enemyRigid = this.GetComponent<Rigidbody> ();
		y0 = this.enemyRigid.position.y;
		this.enemyRigid.velocity = new Vector3(-speed, 0f, 0f);

		base.score = GameObject.Find ("Score").GetComponent<Text> ();
	}

	public override void Move(){
		Vector3 tmp_Pos = this.enemyRigid.position;
		float wav_Theta = (2 * Mathf.PI * (Time.time - wav_Time)) / wav_Freq;
		float wav_sin = Mathf.Sin (wav_Theta);
		tmp_Pos.y = y0 + 0.5f * wav_Len * wav_sin;
		this.enemyRigid.position = tmp_Pos;
	}
	
	// Update is called once per fra
}
