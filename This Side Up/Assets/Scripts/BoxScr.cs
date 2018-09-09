using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(RectTransform))]

public class BoxScr : MonoBehaviour
{
    bool carried;
    Rigidbody2D rb;
    GameObject heldBy;
    public int team;
    BoxCollider2D mCollider;
    BoxCollider2D[] mColliders;
    bool inRange;
    public GameObject redBase, blueBase;
    RectTransform rtr, rtb;
    // Use this for initialization
    void Start ()
    {
        inRange = false;
        team = 0;
        carried = false;
        //CheckColliders();
        mColliders = GetComponents<BoxCollider2D>();
        redBase = GameObject.Find("redBase");
        blueBase = GameObject.Find("blueBase");
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.x <= redBase.transform.position.x + 2.4f
            || transform.position.x >= blueBase.transform.position.x - 2.4f)
            inRange = true;
        else
            inRange = false;
        if (carried)
        {
            gameObject.transform.position = heldBy.transform.position;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "red")
        {
            team = 1;
        }
        else if (collision.tag == "blue")
        {
            team = 2;
        }
        else if (collision.tag == "box" && inRange)
        {
            if (team == 0)
                team = collision.GetComponent<BoxScr>().team;
        }
        else if (!inRange)
            team = 0;

        ColorChange();
    }


    void PickedUp(GameObject player)
    {
        if (!heldBy)
        {
            carried = true;
            heldBy = player;
            team = 0;
            ColorChange();
            foreach (BoxCollider2D bo in mColliders)
            {
                bo.enabled = false;
            }
        }
    }

    void PutDown()
    {
        carried = false;
        heldBy = null;
        foreach (BoxCollider2D bo in mColliders)
        {
            bo.enabled = true;
        }
    }

    void ColorChange()
    {
        if (team == 1)
            GetComponent<SpriteRenderer>().color = Color.red;
        else if (team == 2)
            GetComponent<SpriteRenderer>().color = Color.blue;
        else
            GetComponent<SpriteRenderer>().color = Color.white;
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
                mCollider = myColliders[i];
                break;
            }
        }
    }
}
