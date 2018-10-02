using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierDestroy : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        StartCoroutine("destroyTime");
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
