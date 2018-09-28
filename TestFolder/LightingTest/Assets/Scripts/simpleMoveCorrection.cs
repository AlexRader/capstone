using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMoveCorrection : MonoBehaviour {

    public bool subMove;
    Rigidbody2D rb, subRB;
    public GameObject mySub;
    PlayerMovement scrRef;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.parent = GameObject.FindGameObjectWithTag("sub").transform;
        subMove = false;
        mySub = GameObject.FindGameObjectWithTag("sub");
        scrRef = mySub.GetComponent<PlayerMovement>();
        subRB = mySub.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (subMove)
            scrRef.subSpeed = subRB.velocity;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "sub")
        {
            subMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "sub")
        {
            subMove = false;
        }
    }
}
