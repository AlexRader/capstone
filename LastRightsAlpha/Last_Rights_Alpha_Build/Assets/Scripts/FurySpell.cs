using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurySpell : MonoBehaviour
{
    int dmg;
    GameObject myParent;
    Collider2D[] targetsColliders;
    // Use this for initialization
    void Start ()
    {
        int i = 0;
        targetsColliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        foreach (Collider2D obj in targetsColliders)
        {
            if (obj.gameObject.tag == "Player" && obj.gameObject != myParent)
            {
                obj.SendMessage("takeDamage", dmg);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void parentObj(GameObject var)
    {
        myParent = var;
    }

    void SetDamage(int setDamage)
    {
        dmg = setDamage;
    }

}
