using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwableScr : MonoBehaviour
{
    bool carried;
    Rigidbody2D rb;
    GameObject heldBy;
    CircleCollider2D mCollider;
	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreLayerCollision(12, 12);
        rb = GetComponent<Rigidbody2D>();
        carried = false;
        CheckColliders();
    }

    // Update is called once per frame
    void Update ()
    {
        if (carried)
        {
            gameObject.transform.position = heldBy.transform.position;
        }
	}

    void PickedUp(GameObject player)
    {
        if (!heldBy)
        {
            carried = true;
            heldBy = player;
            mCollider.enabled = false;
        }
    }

    void thrown(Vector2 force)
    {
        carried = false;
        heldBy = null;
        mCollider.enabled = true;
        rb.velocity = force;
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
}
