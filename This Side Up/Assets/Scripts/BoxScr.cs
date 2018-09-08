using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScr : MonoBehaviour
{
    bool carried;
    Rigidbody2D rb;
    GameObject heldBy;
    public int team;
	// Use this for initialization
	void Start ()
    {
        team = 0;
        carried = false;
        //Physics2D.IgnoreLayerCollision(8, 9);
        //Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update ()
    {
        if (carried)
        {
            gameObject.transform.position = heldBy.transform.position;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "red" && !heldBy)
            team = 1;
        else if (collision.tag == "blue" && !heldBy)
            team = 2;
        else if (collision.tag == "box" && !heldBy)
        {
            if (team == 0)
                team = collision.gameObject.GetComponent<BoxScr>().team;
            Debug.Log(team);
        }
        else
            if (collision.tag != "Player") team = 0;
        ColorChange();
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "red" || collision.tag == "blue" 
            || collision.tag == "box")
        {
            team = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    */
    void PickedUp(GameObject player)
    {
        if (!heldBy)
        {
            carried = true;
            heldBy = player;
            team = 0;
            ColorChange();
        }
    }

    void PutDown()
    {
        carried = false;
        heldBy = null;
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

}
