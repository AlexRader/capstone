using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objVelocity : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 vspeed;
    int dmg;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("destroyTime");
        dmg = 10;
	}
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shield")
            rb.velocity = vspeed * -1;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Damage>().SendMessage("takeDamage", dmg);
            Destroy(gameObject);
        }
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(12.0f);
        Destroy(gameObject);
    }


    void vSet(Vector2 vec)
    {
        vspeed = vec;
    }
}
