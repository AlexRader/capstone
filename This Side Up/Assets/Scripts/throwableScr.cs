using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwableScr : MonoBehaviour
{
    bool carried;
    Rigidbody2D rb;
    GameObject heldBy;
    CircleCollider2D mCollider;
    bool Picked, throwing;
    public bool dontTouchMe;

    Vector3 store;

    GameObject player1, player2;
	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreLayerCollision(12, 12);
        rb = GetComponent<Rigidbody2D>();
        carried = false;
        CheckColliders();
        Picked = false;
        dontTouchMe = false;
        throwing = false;

        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player1");
        Physics2D.IgnoreCollision(player1.GetComponent<PlayerScr>().myCollider, mCollider);
        Physics2D.IgnoreCollision(player2.GetComponent<PlayerScr1>().myCollider, mCollider);
    }

    // Update is called once per frame
    void Update ()
    {
        if (carried)
        {
            gameObject.transform.position = heldBy.transform.position;
        }
        if (throwing)
        {
            rb.velocity = store;
            throwing = false;
        }
	}

    void PickedUp(GameObject player)
    {
        if (!heldBy)
        {
            carried = true;
            heldBy = player;
            mCollider.enabled = false;
            Picked = true;            
        }
    }

    void thrown(Vector3 force)
    {
        carried = false;
        heldBy = null;
        mCollider.enabled = true;
        rb.velocity = Vector3.zero;

        store = force;
        throwing = true;
    }

    void CheckColliders()
    {
        int i = 0;
        CircleCollider2D[] myColliders;
        myColliders = GetComponents<CircleCollider2D>();

        foreach (CircleCollider2D bo in myColliders)
        {
            if (!myColliders[i].isTrigger)
            {
                mCollider = myColliders[i];
                break;
            }
            i++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "box" && Picked)
        {
            player1.GetComponent<PlayerScr>().SendMessage("RemoveThrowable", gameObject);
            player2.GetComponent<PlayerScr1>().SendMessage("RemoveThrowable", gameObject);
            Destroy(gameObject);
        }
    }
}
