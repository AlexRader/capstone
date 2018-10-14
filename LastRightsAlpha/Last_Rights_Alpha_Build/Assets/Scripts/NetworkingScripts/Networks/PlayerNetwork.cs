using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;

    public string playerName { get; private set; }

	// Use this for initialization
	void Awake ()
    {
        Instance = this;
        playerName = "DefaultName";

	}
}
