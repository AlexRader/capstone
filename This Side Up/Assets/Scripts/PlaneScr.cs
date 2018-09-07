using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScr : MonoBehaviour
{
    public Rigidbody2D rb;
    public string colObjTag;
    public string colObjTag2;
    public float xVelocity;
    public GameObject box;
    float ypos;
	// Use this for initialization
	void Start ()
    {
        rb.velocity = new Vector2(xVelocity, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == colObjTag)
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, 0);
        }

        if (collision.gameObject.tag == colObjTag2)
        {
            Instantiate(box, new Vector3(rb.transform.position.x, ypos = rb.transform.position.y - .25f, rb.transform.position.z), Quaternion.identity);
        }
    }

}
