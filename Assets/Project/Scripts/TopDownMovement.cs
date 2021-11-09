using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.HID;

[RequireComponent( typeof(BoxCollider))]
public class TopDownMovement : MonoBehaviour, IPunObservable
{
    public float speed = 5;
    public Vector3 movement;
    private PhotonView _photonView;
    private Rigidbody rb;
    public Action<InputAction.CallbackContext> OnTopButtonPress;
    public Action<InputAction.CallbackContext> OnBottomButtonPress;
    public Action<InputAction.CallbackContext> OnRightButtonPress;
    public Action<InputAction.CallbackContext> OnLeftButtonPress;
    [SerializeField] private Vector3 lastReceivedPos;
    [SerializeField] private Quaternion lastReceivedRot;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            Destroy(rb);
        }
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            if (movement.sqrMagnitude > 0)
                transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lastReceivedPos, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lastReceivedRot, 0.05f);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
            rb.velocity = transform.forward * movement.sqrMagnitude * speed;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (_photonView.IsMine)
        {
            var input = ctx.ReadValue<Vector2>();
            movement = new Vector3(input.x, 0, input.y);
        }
    }

    public void OnTop(InputAction.CallbackContext ctx)
    {
        if (_photonView.IsMine)
            OnTopButtonPress?.Invoke(ctx);
    }

    public void OnBottom(InputAction.CallbackContext ctx)
    {
        if (_photonView.IsMine)
            OnBottomButtonPress?.Invoke(ctx);
    }

    public void OnLeft(InputAction.CallbackContext ctx)
    {
        if (_photonView.IsMine)
            OnRightButtonPress?.Invoke(ctx);
    }

    public void OnRight(InputAction.CallbackContext ctx)
    {
        if (_photonView.IsMine)
            OnLeftButtonPress?.Invoke(ctx);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("sending");
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            Debug.Log("receiving");
            lastReceivedPos = (Vector3)stream.ReceiveNext();
            lastReceivedRot = (Quaternion)stream.ReceiveNext();
        }
    }
}