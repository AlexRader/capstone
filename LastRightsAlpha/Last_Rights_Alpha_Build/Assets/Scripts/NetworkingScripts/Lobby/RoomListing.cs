using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text roomNameText;
    private Text RoomNameText { get { return roomNameText; } }

    public string RoomName { get; private set; }

    public bool Updated { get; set; }
    // Use this for initialization
    private void Start ()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();
        Button button = GetComponent<Button>();

        if (lobbyCanvasObj == null)
            return;

        button.onClick.AddListener(() => lobbyCanvas.JoinRoomClick(RoomNameText.text));
	}

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}
