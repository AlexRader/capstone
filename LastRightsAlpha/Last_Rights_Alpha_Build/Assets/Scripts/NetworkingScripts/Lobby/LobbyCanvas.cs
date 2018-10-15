using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup roomLayoutGroup;
    public RoomLayoutGroup RoomLayoutGroup { get { return roomLayoutGroup; } }

    public void JoinRoomClick(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {

        }
        else
        {
            print("join room Failed");
        }
    }
}
