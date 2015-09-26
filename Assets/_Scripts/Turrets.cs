using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turrets : BasicEnemyBehaviour {

	Animator anim;

    // turret speed MUST match the speed of the floor
    public float speed = 3f;

    // Use this for initialization
    void Start ()
    {
        base.score = GameObject.Find("Score").GetComponent<Text>();   
        InvokeRepeating("Fire", 3f, 3f);

		if(this.transform.position.y < 0){
			this.transform.localScale = new Vector3(7,-7,1);
		}

		anim = this.GetComponent<Animator>();		
    }

    public override void Move()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
		if(this.transform.position.y > 0){
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y < 1.5){
				anim.SetFloat("Diff_Y", -2);
			} 
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y > 1.5 && this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y < 5){
				anim.SetFloat("Diff_Y", 0);
			} 
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y > 5){
				anim.SetFloat("Diff_Y", 2);
			} 
		} else {
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y < -5){
				anim.SetFloat("Diff_Y", 2);
			} 
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y > -5 && this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y < -1.5){
				anim.SetFloat("Diff_Y", 0);
			} 
			if(this.transform.position.y - GameObject.Find("Player").GetComponent<Rigidbody>().position.y > -1.5){
				anim.SetFloat("Diff_Y", -2);
			} 
		}

		anim.SetFloat("Diff_X", this.transform.position.x - GameObject.Find("Player").GetComponent<Rigidbody>().position.x);

		
    }
}
