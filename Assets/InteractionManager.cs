using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : NetworkedMonoBehaviour
{
    public GameObject currentTool, currentItem;
    public LayerMask interactionLayer;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && photonView.IsMine)
        {
            TryInteraction();
        }
        

        if (currentTool != null && currentItem != null)
        {
            if (Input.GetKeyDown(KeyCode.I) && photonView.IsMine)
            {
                // objectInteractable.Interact(toolInteractor);
            }
        }
    }

    [PunRPC]
    void InteractRPC(int interactionManagerID, int networkedInteractableID)
    {
        var interactionManager = PhotonView.Find(interactionManagerID).GetComponent<InteractionManager>();
        var interactable = PhotonView.Find(networkedInteractableID).GetComponent<Interactable>();
        interactable.Interact(interactionManager);
    }

    private void TryInteraction()
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit, 2f, interactionLayer))
        {
            var interactable = hit.transform.GetComponent<Interactable>();
            var interactableID = (interactable as NetworkedInteractable).GetComponent<PhotonView>().ViewID;
            photonView.RPC(nameof(InteractRPC), RpcTarget.All, photonView.ViewID, interactableID);
        }
    }


    [PunRPC] // needs reimplementation
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
