using UnityEngine;
using System.Collections;

public class FanWaveBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.childCount == 1){
			this.gameObject.GetComponentInChildren<Fans>().chance = 1f;
		}
	}
}
