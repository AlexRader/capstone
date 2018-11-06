using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurySpellNetworked : MonoBehaviour
{
    int dmg;
    GameObject myParent;
    Collider2D[] targetsColliders;
    // Use this for initialization
    void Start ()
    {
        int i = 0;
        targetsColliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<SpriteRenderer>().bounds.size.x / 2);
        foreach (Collider2D obj in targetsColliders)
        {
            if (obj.isTrigger)
            {
                if (obj.gameObject.tag == "Player" && obj.gameObject != myParent)
                {
                    obj.SendMessage("takeDamage", dmg);
                }
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
