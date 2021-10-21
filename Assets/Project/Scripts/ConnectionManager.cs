using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private string roomName;
    private Transform playerTransform;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("CreateRoom"))
        {
            if (PhotonNetwork.IsConnectedAndReady) PhotonNetwork.CreateRoom(roomName);
        }

        if (GUILayout.Button("JoinRoom"))
        {
            if (PhotonNetwork.IsConnectedAndReady) PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
        }

        roomName = GUILayout.TextField(roomName);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        playerTransform = PhotonNetwork.Instantiate("Cube", Random.insideUnitCircle, Quaternion.identity).transform;
    }

    private void OnDrawGizmos()
    {
        if (playerTransform)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(playerTransform.position + Vector3.up , Vector3.one * .1f);
        }
    }
}