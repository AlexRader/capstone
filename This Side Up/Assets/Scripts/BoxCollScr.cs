using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollScr : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 8);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
