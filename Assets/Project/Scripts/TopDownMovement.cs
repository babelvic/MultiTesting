using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.HID;

public class TopDownMovement : MonoBehaviour
{
    public float speed = 5;
    public Vector3 movement;


    private void Start()
    {
    }

    void Update()
    {
        // var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (movement.sqrMagnitude > 0) transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.position += transform.forward * movement.sqrMagnitude * Time.deltaTime * speed;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log("aa");
    }
}