using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject _toolRef, _objectRef;

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
        

        if (_toolRef != null && _objectRef != null)
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
        if (_toolRef == null) _toolRef = RefDetector(typeof(Interactor));
        if (_objectRef == null) _objectRef = RefDetector(typeof(Interactable));

        if (_toolRef != null)
        {
            networkMonoBehaviourID = _toolRef.GetComponent<NetworkedMonobehaviour>().ID;
            position = transform.position + Vector3.up * 2;
            return;
        }
        
        if (_objectRef != null)
        {
            networkMonoBehaviourID = _objectRef.GetComponent<NetworkedMonobehaviour>().ID;
            position = transform.position + transform.forward * 2;
            return;
        }

        networkMonoBehaviourID = -1;
        position = Vector3.zero;
    }

    public GameObject RefDetector(Type type)
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitInfo, 2f, 1 << LayerMask.NameToLayer("Interactable")))
        {
            return hitInfo.transform.GetComponent(type) != null ? hitInfo.transform.gameObject : null;
        }
        
        return null;
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
                _objectRef = networkMonoBehaviour.gameObject;
                break;
            case Interactor _:
                _toolRef = networkMonoBehaviour.gameObject;
                break;
        }
    }

    [PunRPC]
    public void InteractWithRefs()
    {
        var toolInteractor = _toolRef.GetComponent<Interactor>();
        var objectInteractable = _objectRef.GetComponent<Interactable>();
        var pieceData = toolInteractor?.Interact(objectInteractable);

        if (pieceData)
        {
            Destroy(objectInteractable as Component);
            var piece = _objectRef.AddComponent<Piece>();
            piece.pieceData = pieceData;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }
}
