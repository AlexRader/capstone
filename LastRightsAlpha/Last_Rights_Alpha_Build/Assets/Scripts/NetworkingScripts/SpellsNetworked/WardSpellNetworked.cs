using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardSpellNetworked : MonoBehaviour
{
    public Vector2 vspeed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("destroyTime");
        transform.rotation = Quaternion.LookRotation(Vector3.forward, vspeed.normalized);
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }

    void SetVelocity(Vector2 vec)
    {
        vspeed = vec;
    }
}
