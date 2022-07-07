using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos;
    string nickName;
    bool admin;

    void Start()
    {
        nickName = PhotonNetwork.player.NickName;
        if (nickName == "1234")
        {
            admin = true;
            GetComponent<PlayerCircleMovement>().enabled = false;
        }
        else
        {
            if (photonView.isMine)
            {
                // Activate movement
                GetComponent<PlayerCircleMovement>().enabled = true;
            }
            else
            {
                // Hide player if not owned
                transform.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player, send the others our data
            stream.SendNext(transform.position);
        }
        else
        {
            if(admin == true)
            {
                transform.gameObject.SetActive(true);
                //Network player, receive data
                this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            }
        }
    }
}
