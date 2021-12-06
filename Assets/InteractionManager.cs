using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public GameObject currentTool, currentItem;

    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _photonView.IsMine)
        {
            GetRefs(out var id, out var position);
            
            _photonView.RPC(nameof(FindRefs), RpcTarget.All, id, position);
        }
        

        if (currentTool != null && currentItem != null)
        {
            if (Input.GetKeyDown(KeyCode.I) && _photonView.IsMine)
            {
                // objectInteractable.Interact(toolInteractor);
            }
        }
    }

    private void GetRefs(out int networkMonoBehaviourID, out Vector3 position)
    {
        Debug.Log($"Execute GetRefs in {_photonView.ViewID}");
        var interactable = RefDetector<Interactable>();

        switch (interactable)
        {
            case Tool tool:
                currentTool = tool.gameObject;
                networkMonoBehaviourID = tool.GetComponent<NetworkedMonobehaviour>().ID;
                tool.GetComponent<Collider>().enabled = false;
                position = transform.position + Vector3.up * 2;
                break;
            case  Subpiece subpiece:
                currentItem = subpiece.gameObject;
                subpiece.GetComponent<Collider>().enabled = false;
                networkMonoBehaviourID = subpiece.GetComponent<NetworkedMonobehaviour>().ID;
                position = transform.position + transform.forward * 2;
                break;
            case Workbench _:
                networkMonoBehaviourID = -1;
                position = default;
                break;
            default:
                networkMonoBehaviourID = -1;
                position = Vector3.zero;
                break;
        }
        interactable?.Interact(this);
    }

    public T RefDetector<T>()
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitInfo, 2f, 1 << LayerMask.NameToLayer("Interactable")))
        {
            return hitInfo.transform.GetComponent<T>();
        }
        
        return default;
    }

    [PunRPC]
    public void FindRefs(int id, Vector3 position)
    {
        if (id == -1) return;
        
        var networkMonoBehaviour = FindObjectsOfType<NetworkedMonobehaviour>().First(n => n.ID == id);
        networkMonoBehaviour.transform.position = position;
        networkMonoBehaviour.transform.parent = transform;
        
        switch (networkMonoBehaviour)
        {
            case Interactable _:
                currentItem = networkMonoBehaviour.gameObject;
                break;
            case Interactor _:
                currentTool = networkMonoBehaviour.gameObject;
                break;
        }
    }

    [PunRPC]
    public void InteractWithRefs()
    {
        var toolInteractor = currentTool.GetComponent<Interactor>();
        var objectInteractable = currentItem.GetComponent<Interactable>();
        var pieceData = toolInteractor?.Interact(objectInteractable);

        if (pieceData)
        {
            Destroy(objectInteractable as Component);
            var piece = currentItem.AddComponent<Piece>();
            piece.pieceData = pieceData;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }
}
