using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private Text roomName;
    private Text RoomName { get { return roomName; } }

    public void RoomCreateClick()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if(PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("create successful room");
        }
        else
            print("fuck");

    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed" + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        print("room Made");
    }

}
