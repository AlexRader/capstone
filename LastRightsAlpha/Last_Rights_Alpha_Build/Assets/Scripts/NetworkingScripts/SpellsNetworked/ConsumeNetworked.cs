using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeNetworked : SpellBase
{
    public GameObject spellObj;
    float distance;
    public SpellCasting castRef;
    public int returnTo;

    public float moveSpeed;

    bool activeCasting;
    // Use this for initialization
    void Start()
    {
        Init();
        setCaster(transform.parent);
        activeCasting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (getStartCast())
        {
            StartCoroutine("channeling");
            //castSpell();

            setStartCast();
            activeCasting = true;
        }
        if (activeCasting)
            Aim(castRef.passedInfo.horizontal, castRef.passedInfo.vertical);

    }

    public override void castSpell()
    {
        GameObject temp;
        temp = Instantiate(spellObj, aimReticle.transform.position, Quaternion.identity);
        temp.SendMessage("SetDamage", damage);
        castRef.SendMessage("ResetCasting");
        StartCoroutine("returnCastable");
        activeCasting = false;
    }
    //just make the reticle go to the position specified
    public override void Aim(float x, float y)
    {
        aimReticle.transform.position += new Vector3(x, y, 0).normalized * Time.deltaTime * moveSpeed;
        distance = Vector2.Distance(aimReticle.transform.position, getCaster().transform.position);
        if (distance > radius)
        {
            distance = radius;
            aimReticle.transform.position = getCaster().transform.position +
                (aimReticle.transform.position - getCaster().transform.position).normalized * distance;
        }
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

    IEnumerator channeling()
    {
        yield return new WaitForSeconds(timerMax);
        castSpell();
    }
    public override void Cancel()
    {
        StopCoroutine("channeling");
        castRef.SendMessage("ResetCasting");
        StartCoroutine("returnCastable");
		activeCasting = false;
    }
}
