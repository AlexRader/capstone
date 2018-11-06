using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fury : SpellBase
{

    public GameObject spellObj, vfx;
    GameObject[] circleArr;
    public SpellCasting castRef;
    public int returnTo;
    int i;
    // Use this for initialization
    void Start ()
    {
        circleArr = new GameObject[4];
        i = 0;
        Init();
        setCaster(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (getStartCast())
        {
            castSpell();

            setStartCast();
        }
    }
    public override void castSpell()
    {
        GameObject temp;
        StartCoroutine("CircleSpawner");
        temp = Instantiate(vfx, transform.position, Quaternion.identity);
        temp.transform.parent = gameObject.transform;
    }
    IEnumerator CircleSpawner()
    {
        GameObject temp;
        yield return new WaitForSeconds(timerMax);
        Debug.Log(i);
        if (i == 4)
        {
            Destroy(circleArr[3]);
            castRef.SendMessage("ResetCasting");
            StartCoroutine("returnCastable");
            sendUIMessage(resetTimerMax);
            i = 0;
        }
        else
        {
            ++i;
            SpawnCircles(i);
        }
    }

    void SpawnCircles(int var)
    {
        StartCoroutine("CircleSpawner");

        GameObject temp;

        temp = Instantiate(spellObj, transform.position, Quaternion.identity);
        temp.transform.localScale += new Vector3(var * .5f, var * .5f, 0);
        if (var == 1)
            temp.GetComponent<SpriteRenderer>().color = Color.red;
        else if (var == 2)
            temp.GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (var == 3)
            temp.GetComponent<SpriteRenderer>().color = Color.green;
        else if (var == 4)
        {
            temp.GetComponent<SpriteRenderer>().color = Color.white;

            foreach (GameObject obj in circleArr)
            {
                Destroy(obj);
            }
        }
        temp.transform.parent = gameObject.transform;
        //temp.GetComponent<SpriteRenderer>().sortingOrder = 
        //    GetComponentInParent<SpriteRenderer>().sortingOrder - var - 1;
        if (var != 4)
            temp.SendMessage("SetDamage", (damage - var) * (damage - var));
        else
            temp.SendMessage("SetDamage", var * var);

        temp.SendMessage("parentObj", transform.parent.gameObject);

        circleArr[var - 1] = temp;
    }
    public override void setStartCast()
    {
        casting = !casting;
    }

    public override bool getStartCast()
    {
        return casting;
    }

    void setReturnTo(int i)
    {
        returnTo = i;
    }
    IEnumerator returnCastable()
    {
        yield return new WaitForSeconds(resetTimerMax);

        castRef.SendMessage("setBool", returnTo);
    }
}
