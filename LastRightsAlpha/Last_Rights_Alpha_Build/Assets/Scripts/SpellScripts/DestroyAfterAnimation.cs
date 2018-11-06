using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float TimeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }
}
