using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardSpell : MonoBehaviour
{
    public Vector2 vspeed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("destroyTime");
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, vspeed.normalized);
        transform.Rotate(0, 0, 270);
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void SetVelocity(Vector2 vec)
    {
        vspeed = vec;
    }
}
