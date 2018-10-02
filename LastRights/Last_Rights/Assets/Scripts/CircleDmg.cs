using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDmg : MonoBehaviour
{
    int dmg;
    GameObject myParent;
    Collider2D[] targetsColliders;
    GameObject[] targets;

    // Use this for initialization
    void Start ()
    {
        int i = 0;
        targetsColliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        foreach(Collider2D obj in targetsColliders)
        {
            Debug.Log(obj.gameObject.tag);
            if (obj.gameObject.tag == "Player" && obj.gameObject != myParent)
            {
                obj.SendMessage("takeDamage", dmg);
            }
        }

        foreach(GameObject obj in targets)
        {
            if (obj.tag == "Player" && obj != myParent)
            {
                obj.SendMessage("takeDamage", dmg);
            }
        }
	}

    void myDamage(int i)
    {
        dmg = i;
    }
    void parentObj(GameObject var)
    {
        myParent = var;
    }
}
