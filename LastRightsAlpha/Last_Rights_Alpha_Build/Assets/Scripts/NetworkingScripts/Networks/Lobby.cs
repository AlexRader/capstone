using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        print("connected to server");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

    private void OnConnectedToMaster()
    {
        print("connected to master");

        PhotonNetwork.playerName = PlayerNetwork.Instance.playerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("joined lobby");
    }

}
