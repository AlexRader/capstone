using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobHit : MonoBehaviour
{
    public GameObject lobSplat;
    Rigidbody2D rb;
    public Vector2 vspeed;
    public float vspeedMultiplier;
    int dmg;
    public float timer; 

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("destroyTime");
        dmg = 15;
        rb.velocity = vspeed;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(lobSplat, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Damage>().SendMessage("takeDamage", dmg);
            Instantiate(lobSplat, transform.position, Quaternion.identity);
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
        vspeed = vec * vspeedMultiplier;
    }
}
