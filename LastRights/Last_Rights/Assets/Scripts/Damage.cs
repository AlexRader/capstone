using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Transform startPos;
    GameObject gameControl;
    public int hp, maxHP, team;
    public Text myText;
    public bool res;

    public float maxHpSpeed, timeTillGain;
    public int hpGain;
    // Use this for initialization
    void Start ()
    {
        maxHpSpeed = .4f;
        hpGain = 6;
        timeTillGain = 0;
        res = false;
        gameControl = GameObject.FindGameObjectWithTag("control");
        hp = maxHP;
	}
    void takeDamage(int incomingDamage)
    {
        if (hp > 0 && !res)
            hp -= incomingDamage;
        if (hp <= 0 && !res)
        {
            Debug.Log("happened");
            res = true;
            hp = 0;
            gameControl.SendMessage("setRounds", team);
        }
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
        GetComponent<PlayerMovement>().lobCall = false;
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
}
