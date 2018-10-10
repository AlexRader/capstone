using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeSpell : MonoBehaviour
{

    // Use this for initialization
    public float timeTillDot, maxTime;
    public List<GameObject> myTargets;
    int dmg;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        StartCoroutine("destroyTime");
        dmg = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDot -= Time.deltaTime;
        if (timeTillDot <= 0)
        {
            StartCoroutine("flash");
            foreach (GameObject plr in myTargets)
            {
                plr.GetComponent<Damage>().SendMessage("takeDamage", dmg);
            }
            timeTillDot = maxTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.isTrigger)
        {
            myTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            myTargets.Remove(collision.gameObject);
        }
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

    IEnumerator flash()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.grey;

    }

    void SetDamage(int setDamage)
    {
        dmg = setDamage;
    }
}
