using UnityEngine;
using System.Collections;

public class FloorScroller : MonoBehaviour {

    float speed = 3f;    // speed of the map moving    
    public GameObject map;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
    // initially, the platform floats out and pauses once it's on screen
        if (map.transform.position.x>0) {
            map.transform.position += Time.deltaTime * new Vector3(-speed, 0, 0);
        } else {
            map.transform.position = Vector3.zero;
        }
        
    }
}
