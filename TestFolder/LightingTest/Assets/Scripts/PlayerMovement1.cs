using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    private Rigidbody myRigidbody;
    public float moveSpeed; //Leave this public unless you need it not to be so I can change it in editor - Your Pal, Josh. Or I guess you could just serialize the field or w/e
    public Vector3 subSpeed;
    float horizontal, vertical;
    public bool lockIn;
    GameObject target;
    public float vecZ;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        subSpeed = Vector3.zero;
        lockIn = false;
        target = GameObject.FindGameObjectWithTag("sub");
        vecZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows

        //HandleMovement(horizontal);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            lockIn = !lockIn;
        }
        if (Input.GetKey(KeyCode.J))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            horizontal = 1;
        }
        if (Input.GetKey(KeyCode.I))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            vertical = -1;
        }
        //= Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows // Using Fixed Update for this so that movement speed isn't bound by Framerate

        if (!lockIn)
            HandleMovement(horizontal);
        else
            target.SendMessage("SetJoy", new Vector3(horizontal, 0, vertical));
        horizontal = 0;
        vecZ = 0;
    }

    private void HandleMovement(float horizontal)//So I could use horizontal inputs for movement, I think this works for joystick too, I will test it with ps4 controller
    {
        myRigidbody.velocity = new Vector3(horizontal * moveSpeed, 0, vecZ) + subSpeed; //x = -1 or 1  y = 0 No vertical movement.
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ladder")
            myRigidbody.useGravity = false;
        myRigidbody.velocity = new Vector3(0, 0, 0.2f); //This is (0,0.5) not (0,0) on account of gravity pulling the player down 1, because grav is set to 1. If Grav is changed, set this to half of new value
                                                        //*Edit* It actually had to be 0.2f on account of it being a float. Why, 0.2 specifically, idk but it balanced out... I probably messed gravity up somewhere.
                                                        //Your pal, Josh
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ladder")
            myRigidbody.useGravity = true;
    }
}
