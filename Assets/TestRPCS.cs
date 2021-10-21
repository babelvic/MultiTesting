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
            photonView.RPC("SendInfo", RpcTarget.All);
        }
    }

    [PunRPC]
    void SendInfo()
    {
        Debug.Log(PhotonNetwork.GetPing());
    }
}
