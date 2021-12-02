using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TestRPCS : MonoBehaviour
{
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && photonView.IsMine)
        {
            //Debug.Log("Sending to master");
            photonView.RPC(nameof(SendInfoMaster), RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void SendInfoMaster()
    {
        // Debug.Log("Sending to clients");
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(ReceiveInfoMaster), RpcTarget.All);
        }
    }

    [PunRPC]
    void ReceiveInfoMaster()
    {
        // Debug.Log("Received from Master");
    }
}