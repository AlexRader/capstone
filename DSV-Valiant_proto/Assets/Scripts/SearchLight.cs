using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLight : MonoBehaviour {

    public float curZRotation;
    public float minZRotation = -90;
    public float maxZRotation = 0;

    public bool playerInRange;
    //public float zRotation = 10.0F;
   
    // Use this for initialization
    void Start () {

        //transform.eulerAngles = new Vector3(0, 0, curZRotation);
        // transform.rotation = Quaternion.Euler(0, 0, 0);
        playerInRange = false;
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        lockedRotation();


    }

    private void FixedUpdate()
    {
        if (playerInRange == true)
        {
            if (Input.GetKey(KeyCode.U))
            {
                curZRotation = curZRotation + 1;
            }

            if (Input.GetKey(KeyCode.J))
            {
                curZRotation = curZRotation - 1;
            }
        }
    }

    void lockedRotation()
    {
        curZRotation = Mathf.Clamp(curZRotation, 0, 90);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -curZRotation);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("In LightBox");
            playerInRange = true;

            
        }
   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Exiting LightBox");
            playerInRange = false;


        }
    }

}
