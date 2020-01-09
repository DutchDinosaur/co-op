using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks {

    [SerializeField] byte maxPlayers = 2;
    [SerializeField] GameObject inputPanel;
    [SerializeField] GameObject connecting;

    string gameVersion = "1";
    bool isConnecting;

    private void Start() {
        inputPanel.SetActive(true);
        connecting.SetActive(false);
    }

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect() {
        isConnecting = true;

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

    //public override void OnConnectedToMaster() {
    //    if (isConnecting) {
    //        PhotonNetwork.JoinRandomRoom();
    //    }
    //}

    //public override void OnJoinRandomFailed(short returnCode, string message) {
    //    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });
    //}

    //public override void OnJoinedRoom() {
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
    //        PhotonNetwork.LoadLevel(1);
    //    }
    //}

    //public override void OnDisconnected(DisconnectCause cause) {
    //    inputPanel.SetActive(false);
    //    connecting.SetActive(true);
    //}
}