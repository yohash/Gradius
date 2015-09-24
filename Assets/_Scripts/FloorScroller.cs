using UnityEngine;
using System.Collections;

public class FloorScroller : MonoBehaviour
{
    public GameObject map;

    float speed = 3f;    // speed of the map moving  

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // initially, the platform floats out and pauses once it's on screen
        if (map.transform.position.x > 0)
        {
            map.transform.position += Time.deltaTime * new Vector3(-speed, 0f, 0f);
        }
        else
        {
            map.transform.position = Vector3.zero;
        }
    }
}
