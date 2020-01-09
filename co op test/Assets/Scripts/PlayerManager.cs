using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    public bool IsMine;

    private void Awake() {
        if (IsMine) {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }

        if (photonView.IsMine) {
            PlayerManager.LocalPlayerInstance = this.gameObject;
            IsMine = true;
        }
    }
}