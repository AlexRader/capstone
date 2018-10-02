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
	// Use this for initialization
	void Start ()
    {
        gameControl = GameObject.FindGameObjectWithTag("control");
        hp = maxHP;
	}
    void takeDamage(int incomingDamage)
    {
        hp -= incomingDamage;
        if (hp <= 0)
            gameControl.SendMessage("setRounds", team);
        Debug.Log(hp);
    }

    private void Update()
    {
        myText.text = "HP: " + hp;
    }

    void reset()
    {
        hp = maxHP;
        transform.position = startPos.position;
        GetComponent<PlayerMovement>().lobCall = false;
    }
}
