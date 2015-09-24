using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DarterWaveBehavior : BasicEnemyBehaviour
{

	Animator anim;
    public GameObject enemyDarter;

    // silo speed MUST match the speed of the floor
    public float speed = 3f;

    //Health // we need to make this 3

    // Use this for initialization
    void Start()
    {
		anim = this.GetComponent<Animator>();
        base.score = GameObject.Find("Score").GetComponent<Text>();
        Invoke("SpawnWave", 2f);
        Invoke("SpawnWave", 4f);
    }

    void SpawnWave()
    {
        Invoke("SpawnDarter", 0.25f);
        Invoke("SpawnDarter", 0.5f);
        Invoke("SpawnDarter", 0.75f);
        Invoke("SpawnDarter", 1.0f);
    }

    void SpawnDarter()
    {
        GameObject enemy = Instantiate(enemyDarter) as GameObject;
        Vector3 new_Pos = Vector3.zero;

        new_Pos.x = this.transform.position.x;
        new_Pos.y = this.transform.position.y;

        enemy.transform.position = new_Pos;
    }

    public override void Move()
    {
        this.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
		anim.SetInteger("Life", base.health);
    }
}
