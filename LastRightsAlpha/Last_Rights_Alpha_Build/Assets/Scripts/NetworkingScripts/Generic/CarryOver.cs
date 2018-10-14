using UnityEngine;

public class CarryOver : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(this);
	}
}
