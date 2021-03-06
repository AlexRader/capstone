﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public string colObjTag;
    public Vector2 movement;
    public float jumpForce, xMovement;
    public bool Jumped, held;
    public List<GameObject> boxes;
    public List<GameObject> throwables;
    PlatformEffector2D collisionDetect;

    public BoxCollider2D myCollider;

    GameObject check, checkThrowable;
    bool falling;
    bool isGrounded;
    bool typeHeld;
    bool lifted;

    public float force;
    Vector3 mousePos, direction;
    public GameObject item;
    LineRenderer lr;

    // Use this for initialization
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        typeHeld = false;
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        collisionDetect = GetComponent<PlatformEffector2D>();
        falling = false;
        boxes = new List<GameObject>();
        throwables = new List<GameObject>();
        Jumped = false;
        held = false;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lifted = false;

        force = 0;
		mousePos = Vector3.zero;
        CheckColliders();
    }

    // Update is called once per frame
    void Update ()
    {
        Inputs();
	}

    private void FixedUpdate()
    {
        mousePos.x = Input.GetAxis("RightJoyHor");
        mousePos.y = -Input.GetAxis("RightJoyVert");

        direction = mousePos;
        direction = direction.normalized;
        lr.SetPosition(0, transform.position);

        if (typeHeld && held)
        {
            if (Mathf.Abs(mousePos.x) > 0.2f || Mathf.Abs(mousePos.y) > 0.2f)
                lr.SetPosition(1, (direction * 10));
            else
                lr.SetPosition(1, transform.position);
        }
    }

    void Inputs()
    {
        if (Input.GetAxis("LeftJoyHor") < -.2f && transform.position.x > -9.3)
        {
            rb.velocity = new Vector2(-xMovement, rb.velocity.y);
        }
        if (Input.GetAxis("LeftJoyHor") > 0.2 && transform.position.x < 9.3)
        {
            rb.velocity = new Vector2(xMovement, rb.velocity.y);
        }
        if (Input.GetButtonDown("XButton") && Input.GetAxis("LeftJoyVert") > -0.1f && Jumped == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Jumped = true;
        }
        if (Input.GetButtonDown("OButton") && !isGrounded)
        {
            Jumped = true;
            StartCoroutine("Fall");
            falling = true;
        }
        if (Input.GetAxis("LeftJoyHor") > -.1f && Input.GetAxis("LeftJoyHor") < 0.1)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if (Input.GetButtonDown("Square"))
        {
            if (boxes.Count > 0 && !held)
            {
                check = FindClosestBox();
                Debug.Log(check);
                if ((check.transform.position - transform.position).sqrMagnitude < 4*3)
                {
                    check.SendMessage("PickedUp", gameObject);
                    held = true;
                    typeHeld = false;
                }
            }

            else if (held)
            {
                check.SendMessage("PutDown", gameObject);
                held = false;
            }
        }
        else if(Input.GetButtonDown("R2"))
        {
            if (throwables.Count > 0 && !held)
            {
                checkThrowable = findClosestThrowable();

                if ((checkThrowable.transform.position - transform.position).sqrMagnitude < 4 * 4)
                {
                    checkThrowable.SendMessage("PickedUp", gameObject);
                    held = true;
                    typeHeld = true;
                    lr.enabled = true;
                }
            }
            else if (!typeHeld && held)
            {
                check.SendMessage("SpawnBall", item);
            }
        }
        if (Input.GetButtonUp("R2") && checkThrowable != null)
        {
            if (lifted)
            {
                mousePos.x = Input.GetAxis("RightJoyHor");
                mousePos.y = -Input.GetAxis("RightJoyVert");
                direction = mousePos;
                direction = direction.normalized;// * force;

                checkThrowable.SendMessage("thrown", direction);
                checkThrowable = null;
                held = false;

                lr.enabled = false;
                lifted = false;
            }
            else
                lifted = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == colObjTag 
            || collision.gameObject.tag == "box") && falling == false)
        {
            Jumped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
            boxes.Add(collision.gameObject);
        if (collision.gameObject.tag == "throw")
            throwables.Add(collision.gameObject);
        if (collision.gameObject.tag == "ground")
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
            boxes.Remove(collision.gameObject);
        if (collision.gameObject.tag == "throw")
            throwables.Remove(collision.gameObject);
        if (collision.gameObject.tag == "ground")
            isGrounded = false;
    }

    GameObject FindClosestBox()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in boxes)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    GameObject findClosestThrowable()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in throwables)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void RemoveThrowable(GameObject sentObj)
    {
        throwables.Remove(sentObj);
    }
    IEnumerator Fall()
    {
        collisionDetect.surfaceArc = 0;
        yield return new WaitForSeconds(.4f);
        collisionDetect.surfaceArc = 60;
        Jumped = false;
        falling = false;
    }

    void CheckColliders()
    {
        int i = 0;
        BoxCollider2D[] myColliders;
        myColliders = GetComponents<BoxCollider2D>();

        foreach (BoxCollider2D bo in myColliders)
        {
            if (!myColliders[i].isTrigger)
            {
                myCollider = myColliders[i];
                break;
            }
        }
        Debug.Log(myCollider);
    }
}
