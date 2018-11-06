using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingReticle : MonoBehaviour
{
    public GameObject targetObject;
    Vector2 currentRotation;
    Vector3 pos;
    Quaternion quat;
    private void Start()
    {
        currentRotation = new Vector2(transform.rotation.x, transform.rotation.y);
    }
    private void Update()
    {
        pos = targetObject.transform.position - transform.position;
        Debug.Log(pos);
        if (Mathf.Abs(pos.x) < 1 && Mathf.Abs(pos.y) < 1)
        {

        }
        else
        {
            float angle = (Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg) -44;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            quat = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 0);
        }
    }
}
