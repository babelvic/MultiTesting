using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private string roomName;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("CreateRoom"))
        {
            if(PhotonNetwork.IsConnectedAndReady) PhotonNetwork.CreateRoom(roomName);
        }   
        if (GUILayout.Button("JoinRoom"))
        {
            if(PhotonNetwork.IsConnectedAndReady) PhotonNetwork.JoinOrCreateRoom(roomName,null, null);
        }

        roomName = GUILayout.TextField(roomName);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
    }
}
