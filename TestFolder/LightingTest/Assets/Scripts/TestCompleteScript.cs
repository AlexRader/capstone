using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompleteScript : MonoBehaviour {

	public GameObject WinText;

	// Use this for initialization
	void Start () {
		WinText.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Log ("It's all over");
			WinText.SetActive (true);
		} 
	}
}
