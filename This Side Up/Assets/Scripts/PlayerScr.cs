using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    public Rigidbody2D rb;
    public string colObjTag;
    public Vector2 movement;
    public float jumpForce, xMovement;
    bool Jumped;
	// Use this for initialization
	void Start ()
    {
        Jumped = false;
	}
	
	// Update is called once per frame
	void Update () {
        Inputs();
	}

    void Inputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-xMovement, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(xMovement, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.W) && Jumped == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Jumped = true;
        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == colObjTag)
        {
            Jumped = false; 
        }
    }
}
