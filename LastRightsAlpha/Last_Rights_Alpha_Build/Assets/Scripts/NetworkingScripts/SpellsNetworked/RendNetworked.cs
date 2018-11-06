using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RendNetworked : SpellBase
{
    private PhotonView photonView;

    public GameObject spellObj;
    Vector2 direction;
    public SpellCasting castRef;
    public int returnTo;
    // Use this for initialization
    void Start ()
    {
        photonView = GetComponent<PhotonView>();

        Init();
        setCaster(transform.parent);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (getStartCast())
        {
            Aim(castRef.passedInfo.horizontal, castRef.passedInfo.vertical);

            castSpell();

            setStartCast();
        }
    }

    public override void castSpell()
    {
        Debug.Log("breakHere?");
        if (photonView.isMine)
            photonView.RPC("RPC_CreateSpell", PhotonTargets.All);

    }
    //just make the reticle go to the position specified
    public override void Aim(float x, float y)
    {
        aimReticle.transform.position = (new Vector3(x, y, 0).normalized * radius) + getCaster().position;
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
    [PunRPC]
    private void RPC_CreateSpell()
    {
        if (photonView.isMine)
        {
            GameObject temp;
            direction = aimReticle.transform.position - transform.position;
            temp = PhotonNetwork.Instantiate(Path.Combine(Path.Combine("Prefabs", "NetworkedSpells"), "RendNetworked"), aimReticle.transform.position, Quaternion.LookRotation(direction), 0);
            temp.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            temp.SendMessage("SetVelocity", direction);
            temp.SendMessage("SetDamage", damage);
            temp.SendMessage("parentObj", transform.parent.gameObject);
            castRef.SendMessage("ResetCasting");
            StartCoroutine("returnCastable");
        }
    }
}
