using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class TopDownMovement : MonoBehaviour
{
    public float speed = 5;
    public Vector3 movement;
    private Rigidbody rb;
    public Action<InputAction.CallbackContext> OnTopButtonPress;
    public Action<InputAction.CallbackContext> OnBottomButtonPress;
    public Action<InputAction.CallbackContext> OnRightButtonPress;
    public Action<InputAction.CallbackContext> OnLeftButtonPress;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (movement.sqrMagnitude > 0) transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * movement.sqrMagnitude * speed;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        var input = ctx.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0, input.y);
    }

    public void OnTop(InputAction.CallbackContext ctx)
    {
        OnTopButtonPress?.Invoke(ctx);
    }

    public void OnBottom(InputAction.CallbackContext ctx)
    {
        OnBottomButtonPress?.Invoke(ctx);
    }

    public void OnLeft(InputAction.CallbackContext ctx)
    {
        OnRightButtonPress?.Invoke(ctx);
    }

    public void OnRight(InputAction.CallbackContext ctx)
    {
        OnLeftButtonPress?.Invoke(ctx);
    }
}