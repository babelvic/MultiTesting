using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : NetworkedMonoBehaviour
{
    public ObjectData currentTool, currentPieceOrSubpiece;
    public LayerMask interactionLayer;
    public LayerMask dropLayer;
    private PhotonView photonView;

    public int currentItemID;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && photonView.IsMine)
        {
            TryPickup();
        }

        if (Input.GetKeyDown(KeyCode.Q) && photonView.IsMine)
        {
            TryDrop();
        }


        // if (currentTool != null && currentItem != null)
        // {
        //     if (Input.GetKeyDown(KeyCode.I) && photonView.IsMine)
        //     {
        //         // objectInteractable.Interact(toolInteractor);
        //     }
        // }
    }


    private void TryPickup()
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit, 2f, interactionLayer))
        {
            int managerID = photonView.ViewID;
            if (hit.transform.TryGetComponent<Item>(out var item))
            {
                currentItemID = item.GetComponent<PhotonView>().ViewID;
                photonView.RPC(nameof(PickupRPC), RpcTarget.All, managerID, currentItemID);
            }
        }
        
        // itemSet.Add(hit.transform.GetComponent<>());

        // var pickupable = hit.transform.GetComponent<NetworkedPickupable>();
        // var pickupableID = pickupable.GetComponent<PhotonView>().ViewID;
    }

    private void TryDrop()
    {
        if (currentPieceOrSubpiece == null) return;
        int managerID = photonView.ViewID;
        int dropObjectID = -1;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit, 2f, dropLayer))
        {
            if(hit.transform.TryGetComponent<IObjectDropable>(out var dropable))
                dropObjectID = (dropable as Component).GetComponent<PhotonView>().ViewID;
        }
        
        photonView.RPC(nameof(DropRPC), RpcTarget.All, managerID, dropObjectID);
        
        // int id;
        // if (currentItem) id = currentItem.GetComponent<PhotonView>().ViewID;
        // else if (currentTool) id = currentTool.GetComponent<PhotonView>().ViewID;
        // else return;
        //
        // photonView.RPC(nameof(DropRPC), RpcTarget.All,photonView.ViewID ,id);
    }

    [PunRPC]
    void PickupRPC(int interactionManagerID, int itemID)
    {
        var interactionManager = PhotonView.Find(interactionManagerID).GetComponent<InteractionManager>();
        var item = PhotonView.Find(itemID).GetComponent<Item>();

        switch (item.itemData)
        {
            case ToolData toolData:
                interactionManager.currentTool ??= toolData;
                interactionManager.GetComponent<MeshFilter>().sharedMesh = item.GetComponent<MeshFilter>().sharedMesh;
                Destroy(item.gameObject);
                break;
            case SubpieceData subpieceData:
                interactionManager.currentPieceOrSubpiece ??= subpieceData;
                break;
            case PieceData pieceData:
                interactionManager.currentPieceOrSubpiece ??= pieceData;
                break;
        }

        if (item)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<Collider>().enabled = false;
            item.transform.SetParent(interactionManager.transform);
        }


        // var interactionManager = PhotonView.Find(interactionManagerID).GetComponent<InteractionManager>();
        // var pickupable = PhotonView.Find(networkedInteractableID).GetComponent<NetworkedPickupable>();
        // pickupable.OnPickup(interactionManager); // surface missing;
    }

    [PunRPC]
    void DropRPC(int interactionManagerID, int dropObjectID)
    {
        var interactionManager = PhotonView.Find(interactionManagerID).GetComponent<InteractionManager>();
        var itemObject = PhotonView.Find(interactionManager.currentItemID).GetComponent<Item>();

        IObjectDropable dropObject = null;

        if (dropObjectID != -1)
        {
            dropObject = PhotonView.Find(dropObjectID).GetComponent<IObjectDropable>();
        }

        switch (dropObject)
        {
            case Workbench workbench:
                workbench.SetSubpiece(itemObject);
                break;
            case Surface surface:
                //lo mismo con surface
                break;
            case null:
                itemObject.transform.SetParent(null);
                itemObject.GetComponent<Rigidbody>().isKinematic = false;
                itemObject.GetComponent<Collider>().enabled = true;
                break;
        }
        interactionManager.currentPieceOrSubpiece = null;
        
        // var interactionManager = PhotonView.Find(interactionManagerID).GetComponent<InteractionManager>();
        // var pickupable = PhotonView.Find(networkedInteractableID).GetComponent<NetworkedPickupable>();
        // pickupable.OnDrop(interactionManager, null);
    }


    [PunRPC] // needs reimplementation
    public void InteractWithRefs()
    {
        // var toolInteractor = currentTool.GetComponent<Interactor>();
        // var objectInteractable = currentItem.GetComponent<Processable>();
        // var pieceData = toolInteractor?.Process(objectInteractable);
        //
        // if (pieceData)
        // {
        //     Destroy(objectInteractable as Component);
        //     var piece = currentItem.AddComponent<Piece>();
        //     piece.pieceData = pieceData;
        // }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }
}