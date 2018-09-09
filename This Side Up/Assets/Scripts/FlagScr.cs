using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScr : MonoBehaviour
{
    public int team;

    public GameObject objDir;

    RaycastHit2D hit;

    public float currentHeight;

    public bool move;

    public bool moveDown;

    public Rigidbody2D rb;

    int layermask = 1 << 9;

    // Use this for initialization
    void Start ()
    {
        move = false;
        moveDown = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentHeight = transform.position.y;
        checkHeight();
        if (move)
            rb.velocity = new Vector2(rb.velocity.x, .2f);
        else if (moveDown)
            rb.velocity = new Vector2(rb.velocity.x, -.2f);
        else if (!move)
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }


    void checkHeight()
    {

        hit = Physics2D.Raycast(transform.position, objDir.transform.position - transform.position, Mathf.Infinity, layermask);
        Debug.DrawRay(transform.position, transform.right, Color.green, Mathf.Infinity);
//        Debug.Log(hit.collider.tag);

        //hit.transform.gameObject.tag == "box" && hit.transform.gameObject.GetComponent<BoxScr>().team == team
        if (hit == false)
        {
            move = false;
        }
        else if (hit.collider.tag == "box" && hit.transform.gameObject.GetComponent<BoxScr>().team == team)
        {
            move = true;
            moveDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "throw" && !move)
        {
            Debug.Log("here");
            moveDown = true;
        }
    }
}
