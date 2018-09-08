using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    public Rigidbody2D rb;
    public string colObjTag;
    public Vector2 movement;
    public float jumpForce, xMovement;
    public bool Jumped, held;
    public List<GameObject> boxes;
    PlatformEffector2D collisionDetect;

    GameObject check;
    bool falling;
    bool isGrounded;
    // Use this for initialization
    void Start ()
    {
        isGrounded = true;
        collisionDetect = GetComponent<PlatformEffector2D>();
        falling = false;
        boxes = new List<GameObject>();
        Jumped = false;
        held = false;
	}
	
	// Update is called once per frame
	void Update () {
        Inputs();
	}

    void Inputs()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > -9.3)
        {
            rb.velocity = new Vector2(-xMovement, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < 9.3)
        {
            rb.velocity = new Vector2(xMovement, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.W) && Jumped == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Jumped = true;
        }
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            Jumped = true;
            StartCoroutine("Fall");
            falling = true;
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (boxes.Count > 0 && !held)
            {
                check = FindClosestBox();
                //Debug.Log(check);
               // Debug.Log((check.transform.position - transform.position).sqrMagnitude);
                if ((check.transform.position - transform.position).sqrMagnitude < 4*4)
                {
                    check.SendMessage("PickedUp", gameObject);
                    held = true;
                }
            }
            else if (held)
            {
                check.SendMessage("PutDown", gameObject);
                held = false;
            }
            //    GameObject.FindGameObjectsWithTag
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
            boxes.Remove(collision.gameObject);
        if (collision.gameObject.tag == "ground")
            isGrounded = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            collisionDetect.surfaceArc = 60;
        }
    }

    GameObject FindClosestBox()
    {
        //GameObject[] Box;
        //Box = GameObject.FindGameObjectsWithTag("box");
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
        //Debug.Log("returned");
        //Debug.Log(closest);
        return closest;
    }

    IEnumerator Fall()
    {
        collisionDetect.surfaceArc = 0;
        yield return new WaitForSeconds(.4f);
        collisionDetect.surfaceArc = 60;
        Jumped = false;
        falling = false;
    }
}
