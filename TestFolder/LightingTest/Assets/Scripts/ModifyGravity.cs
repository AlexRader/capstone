using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyGravity : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Physics.gravity = new Vector3(0, 0, -100.0F);
    }
}
