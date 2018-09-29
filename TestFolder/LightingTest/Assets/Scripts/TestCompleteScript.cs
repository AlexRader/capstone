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
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Log ("It's all over");
			WinText.SetActive (true);
            StartCoroutine("GOTOform");
		} 
	}
    IEnumerator GOTOform()
    {
        yield return new WaitForSeconds(4.0f);
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSe9eLqVE1EAcKj0zBA8DRJshlZzwzLqfajGIIGG5kc4cxSOqA/viewform");
        Application.Quit();
    }
}
