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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("SendInfoMaster", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void SendInfoMaster(PhotonMessageInfo info)
    {
        photonView.RPC("ReceiveInfoMaster", RpcTarget.All, info.Sender.ActorNumber);
    }
    
    [PunRPC]
    void ReceiveInfoMaster(int actor)
    {
        Debug.Log(actor);
    }
}
