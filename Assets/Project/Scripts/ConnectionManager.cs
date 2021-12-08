using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private string roomName;
    private Transform playerTransform;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.InRoom)
        {
            playerTransform = PhotonNetwork.Instantiate("P1", transform.position, Quaternion.identity).transform;
        }
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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerTransform)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(playerTransform.position + Vector3.up, Vector3.one * .1f);
        }
    }
}