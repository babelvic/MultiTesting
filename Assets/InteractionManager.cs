using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject _toolRef, _objectRef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetRefs();
        }
        

        if (_toolRef != null && _objectRef != null)
        {
            if (Input.GetKeyDown(KeyCode.I))
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
                
                // objectInteractable.Interact(toolInteractor);
            }
        }
    }
    
    public void GetRefs()
    {
        if(_toolRef == null) _toolRef = RefDetector(typeof(Interactor));
        if(_objectRef == null) _objectRef = RefDetector(typeof(Interactable));

        if (_toolRef != null)
        {
            _toolRef.transform.position = transform.position + Vector3.up * 2;
            _toolRef.transform.parent = transform;
        }

        if (_objectRef != null)
        {
            _objectRef.transform.position = transform.position + transform.forward * 2;
            _objectRef.transform.parent = transform;
        }
    }

    public GameObject RefDetector(Type type)
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitInfo, 2f, 1 << LayerMask.NameToLayer("Interactable")))
        {
            return hitInfo.transform.GetComponent(type) != null ? hitInfo.transform.gameObject : null;
        }
        
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }
}
