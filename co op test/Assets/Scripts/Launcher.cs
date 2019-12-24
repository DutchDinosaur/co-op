using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks {

    [SerializeField] byte maxPlayers = 2;
    [SerializeField] GameObject inputPanel;
    [SerializeField] GameObject connecting;

    string gameVersion = "1";

    private void Start() {
        inputPanel.SetActive(true);
        connecting.SetActive(false);
    }

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect() {
        inputPanel.SetActive(false);
        connecting.SetActive(true);
        if (PhotonNetwork.IsConnected) {
        PhotonNetwork.JoinRandomRoom();
        }
        else {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() {
        Debug.Log("connected");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("creating new room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });
    }

    public override void OnJoinedRoom() {
        Debug.Log("joined room");
    }

    public override void OnDisconnected(DisconnectCause cause) {
        inputPanel.SetActive(false);
        connecting.SetActive(true);
    }
}