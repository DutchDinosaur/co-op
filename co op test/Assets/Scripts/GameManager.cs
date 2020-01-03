using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks {

    public GameObject playerPrefab;

    private void Start() {
        if (PlayerManager.LocalPlayerInstance == null) {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            Debug.Log("instantiating " + PlayerManager.LocalPlayerInstance.name);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            LeaveRoom();
        }
    }

    public override void OnLeftRoom() {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena() {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnPlayerEnteredRoom(Player other) {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

        if (PhotonNetwork.IsMasterClient) {
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other) {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);

        if (PhotonNetwork.IsMasterClient) {
            LoadArena();
        }
    }
}