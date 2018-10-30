using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : Photon.MonoBehaviour
{
    public Transform startPos;
    GameObject gameControl;
    public int hp, maxHP, team;
    public Text myText;
    public bool res;

    public float maxHpSpeed, timeTillGain;
    public int hpGain;

    public int Health;
    int savedDamage;
    // Use this for initialization
    void Start()
    {
        maxHpSpeed = .4f;
        hpGain = 6;
        timeTillGain = 0;
        res = false;
        gameControl = GameObject.FindGameObjectWithTag("Control");
        hp = maxHP;
    }
    void takeDamage(int incomingDamage)
    {
        if (hp > 0 && !res)
        {
            hp -= incomingDamage;
            savedDamage = incomingDamage;
            photonView.RPC("RPC_TakeDamage", PhotonTargets.All, savedDamage);
        }
        if (hp <= 0 && !res)
        {
            res = true;
            hp = 0;
            gameControl.SendMessage("setRounds", team);
        }
        GetComponent<SpellCasting>().SendMessage("CancelCasting");
    }

    private void Update()
    {
        myText.text = "HP: " + hp;
    }

    void reset()
    {
        res = false;
        hp = maxHP;
        transform.position = startPos.position;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Damage>().team == team)
        {
            if (res)
            {
                timeTillGain += Time.deltaTime;
                if (timeTillGain >= maxHpSpeed)
                {
                    timeTillGain = 0;
                    hp += hpGain;
                }
                if (hp >= maxHP / 2)
                {
                    hp = maxHP / 2;
                    res = false;
                    gameControl.SendMessage("AddPlayerBack", team);
                }
            }
        }
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    [PunRPC]
    private void RPC_TakeDamage(int inputDamage)
    {
        Debug.Log("called");
        takeDamage(inputDamage);
    }
}
