using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLight : MonoBehaviour {

    public float curZRotation;
	public float minZRotation;
	public float maxZRotation;

	public float increaseAngle;
	public float decreaseAngle;


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
	//	curZRotation = (curZRotation > 180) ? curZRotation - 360 : curZRotation;


    }

    private void FixedUpdate()
    {
        if (playerInRange == true)
        {
            if (Input.GetKey(KeyCode.U))
            {
				curZRotation = curZRotation + increaseAngle;
            }

            if (Input.GetKey(KeyCode.J))
            {
				curZRotation = curZRotation + decreaseAngle;
            }
        }
    }

    void lockedRotation()
    {
		curZRotation = Mathf.Clamp(curZRotation, minZRotation, maxZRotation);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -curZRotation);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           // Debug.Log("In LightBox");
            playerInRange = true;

            
        }
   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Exiting LightBox");
            playerInRange = false;


        }
    }

}
