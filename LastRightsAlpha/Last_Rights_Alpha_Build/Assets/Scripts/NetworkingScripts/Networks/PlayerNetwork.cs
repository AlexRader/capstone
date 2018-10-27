using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;

    public string playerName { get; private set; }

    private PhotonView photonView;
    private int numPlayers;
	// Use this for initialization
	void Awake ()
    {
        Instance = this;
        playerName = "DefaultName";

        photonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 90;
        PhotonNetwork.sendRateOnSerialize = 60;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "NetTesting")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        Debug.Log("asdasda");

        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        Debug.Log("plz");
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    [PunRPC]//need this boi
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        ++numPlayers;
        if (numPlayers == PhotonNetwork.playerList.Length)
        {
            print("all are in game");
            photonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
        else
        {
            print("no");
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine(Path.Combine("Prefabs", "NetworkedPlayer"), "NetworkedPlayer"), Vector3.zero, Quaternion.identity, 0);
    }

}
