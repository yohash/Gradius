using UnityEngine;
using System.Collections;

public class TitleScreenBehaviour : MonoBehaviour {
	GameObject Title;

	GameObject Cursor;

	bool Classic = true;

	// Use this for initialization

	void Start () {
		Title = GameObject.Find("Title");
		Cursor = GameObject.Find("Cursor");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Input.GetKeyDown(KeyCode.RightShift)){
			Classic = !Classic;
			if(Classic == true){
				Cursor.transform.position = new Vector3(-3.3f, 0.35f, 0f);
			} else {
				Cursor.transform.position = new Vector3(-3.3f, -1.1f, 0f);
			}
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			if(Classic == true){
				Application.LoadLevel("Scene_0");
			} else {
				Application.LoadLevel("Scene_1");
			}
		}
	}
}
