using UnityEngine;
using System.Collections;


public class ATST2DColl : MonoBehaviour {

    GameObject atst;
    Rigidbody2D rb2D;
    Vector3 atstTarget = Vector3.zero;

    int isTop = 1; // 1 for top, -1 for bottom
        

    // Use this for initialization
    void Start()
    {
        atst = this.transform.parent.gameObject;
        this.transform.position = atst.transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        if (this.transform.position.y < 1.25f) { isTop = -1; }        // on the bottom
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // top walker hits a top-mountain
        if (coll.gameObject.tag == "Mountain")
        {
            InvokeRepeating("moveDown", 0f, 0.001f);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        // top walker hits a top-mountain
        if (coll.gameObject.tag == "Mountain")
        {
            CancelInvoke("moveDown");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = atst.transform.position;
        atstTarget = atst.GetComponent<ATSTBehaviour>().targetX;

        // layer 31 is the current layer for 'ground'
        // 'ground' also defines mountains

        if (atst.GetComponent<ATSTBehaviour>().thisStatus == ATSTstatus.seeking || atst.GetComponent<ATSTBehaviour>().thisStatus == ATSTstatus.leaving)
        {
            // we are climbing, gotta check to see if we should move down
            int layerMask = 1 << 31;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(0, 1f * isTop), Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.y - this.transform.position.y);
                atst.GetComponent<ATSTBehaviour>().adjustY(atst.transform.position.y + (distance - 0.65f) * isTop);
                print(distance);
            }
            //enum ATSTstatus { seeking, firing, chill, leaving };
        }
    }

    void moveDown() {
        atst.GetComponent<ATSTBehaviour>().adjustY(atst.transform.position.y - 0.1f*isTop);
    }
}
