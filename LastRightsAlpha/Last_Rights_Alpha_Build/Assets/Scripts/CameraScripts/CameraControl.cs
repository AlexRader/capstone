using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject playerRef;
    public Vector3 offset;
    public float speed;

    private void FixedUpdate()
    {
        SmoothLerp();
    }

    void SmoothLerp()
    {
        transform.position = Vector3.Lerp(transform.position, playerRef.transform.position, speed * Time.deltaTime) + offset;
    }
}
