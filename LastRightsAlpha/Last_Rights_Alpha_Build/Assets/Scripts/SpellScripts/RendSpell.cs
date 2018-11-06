using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendSpell : MonoBehaviour
{
    public Rigidbody2D rb;
    GameObject myParent;

    public Vector2 vspeed;
    int dmg;
    public float objVelocity;
    bool called;


    // Use this for initialization
    void Start()
    {
        called = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("destroyTime");
        rb.velocity = vspeed.normalized * objVelocity;

    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            rb.velocity *= -1;
            myParent = gameObject;
        }
        else if (collision.gameObject.tag == "Wall" || (collision.gameObject.tag == "MainCapture" && !collision.isTrigger))
            Destroy(gameObject);
        else
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject != myParent)
            {
                if (!called)
                {
                    Debug.Log("dsgfsdf");
                    Debug.Log(dmg);
                    called = true;
                    collision.gameObject.GetComponent<Damage>().SendMessage("takeDamage", dmg);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCapture")
            Destroy(gameObject);
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(12.0f);
        Destroy(gameObject);
    }
    void SetVelocity(Vector2 vec)
    {
        vspeed = vec;
    }
    void SetDamage(int setDamage)
    {
        dmg = setDamage;
    }
    void parentObj(GameObject var)
    {
        myParent = var;
    }
}


