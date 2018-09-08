using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreColScr : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreLayerCollision(9, 11);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
