using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldToggling : MonoBehaviour {

    float toggleTime = 1f;      // 1sec hold-down to toggle
    float startTime;

    public float camH, camW;

    Text helperText;
    Color hcolor;

    GameObject customShieldBlue, customShieldRed;     // links to the halo that color-codes the ship
    public bool isBlue;                                     // Use this for initialization
    bool spaceDown;

    // need to know this
    public bool isPlayerDead = false;

    void Awake ()
    {
        Transform customShieldTrans = transform.FindChild("Custom_Shield_Blue");
        customShieldBlue = customShieldTrans.gameObject;
        customShieldBlue.SetActive(false);

        customShieldTrans = transform.FindChild("Custom_Shield_Red");
        customShieldRed = customShieldTrans.gameObject;
        customShieldRed.SetActive(false);
        
        helperText = GameObject.Find("Helper_Text").GetComponent<Text>();
        helperText.color = Color.clear ;
    }
	
    public void StartToggle()
    {
        customShieldBlue.SetActive(true);
        customShieldRed.SetActive(false);
        isBlue = true;
        helperText.color = Color.white;
        helperText.text = "You are now\nimmune to blue\ndamage sources!";
        Invoke("clearHelperText", 4f);
        Invoke("changeHelperText", 10f);

    }


	// Update is called once per frame
	void Update ()
    {
        isPlayerDead = GetComponentInParent<PlayerController>().dead;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            spaceDown = true;
        }
        if (Input.GetKey(KeyCode.Space) && (Time.time - startTime) > toggleTime && spaceDown && !isPlayerDead)
        {
            if (isBlue)
            {
                Transform customShieldTrans = transform.FindChild("Custom_Shield_Blue");
                customShieldBlue = customShieldTrans.gameObject;
                customShieldBlue.SetActive(false);
                customShieldTrans = transform.FindChild("Custom_Shield_Red");
                customShieldRed = customShieldTrans.gameObject;
                customShieldRed.SetActive(true);
                isBlue = false;

                spaceDown = false;
            } else
            {
                Transform customShieldTrans = transform.FindChild("Custom_Shield_Red");
                customShieldRed = customShieldTrans.gameObject;
                customShieldRed.SetActive(false);
                customShieldTrans = transform.FindChild("Custom_Shield_Blue");
                customShieldBlue = customShieldTrans.gameObject;
                customShieldBlue.SetActive(true);
                isBlue = true;

                spaceDown = false;
            }
        }
    }

    public void clearShieldsOnDeath()
    {
        Transform customShieldTrans = transform.FindChild("Custom_Shield_Blue");
        customShieldBlue = customShieldTrans.gameObject;
        customShieldBlue.SetActive(false);
        customShieldTrans = transform.FindChild("Custom_Shield_Red");
        customShieldRed = customShieldTrans.gameObject;
        customShieldRed.SetActive(false);
    }

    void changeHelperText()
    {
        helperText.color = Color.white;
        helperText.text = "Hold (SPACE)\nto change shield\ncolors, swapping\n immunity";
        Invoke("clearHelperText", 4f);
    }
    void clearHelperText()
    {
        helperText.text = "";
        helperText.color = Color.clear;
    }
}
