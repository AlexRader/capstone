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

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        numPlayers = 1;
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
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
        }
        else
        {
            print("no");

        }
    }

}
