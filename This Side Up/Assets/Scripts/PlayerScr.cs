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
    public List<GameObject> throwables;
    PlatformEffector2D collisionDetect;

    GameObject check, checkThrowable;
    bool falling;
    bool isGrounded;
    bool typeHeld;

    public float force;
    Vector3 mousePos, direction;
    public GameObject item;
    LineRenderer lr;

    // Use this for initialization
    void Start ()
    {
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
        force = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rb.velocity.x > (xMovement + xMovement) 
            || rb.velocity.x < -(xMovement * 2))
            rb.velocity = Vector2.zero;
        Inputs();

    }

    private void FixedUpdate()
    {
        mousePos = Camera.main.WorldToScreenPoint(transform.position);
        direction = (Input.mousePosition - mousePos);
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
        else if(Input.GetMouseButtonDown(1))
        {
            if (throwables.Count > 0 && !held)
            {
                checkThrowable = findClosestThrowable();

                if ((checkThrowable.transform.position - transform.position).sqrMagnitude < 4 * 3)
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
        if (Input.GetMouseButton(1) && checkThrowable != null)
        {
            if (force < 18)
                force += .3f;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, direction - transform.position);
        }
        if (Input.GetMouseButtonUp(1) && checkThrowable != null)
        {
            
            direction = direction.normalized;
            direction *= force;

            checkThrowable.SendMessage("thrown", direction);
            checkThrowable = null;
            held = false;

            lr.enabled = false;
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
        Debug.Log("here");
        throwables.Remove(sentObj);
    }
    void RemoveThrowable1(GameObject sentObj)
    {
        Debug.Log("here");
        lr.enabled = false;
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
}
