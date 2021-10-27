using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class OilUpMinigame : MonoBehaviour
{
    [Range(0, 1), SerializeField] private float percent = 0;
    [SerializeField] private float time = 3;
    private bool inside;
    private Transform other;
    public GameObject splurge;

    private void Update()
    {
        if (inside)
        {
            if (percent >= 1)
            {
                inside = false;
                return;
            }

            if (time <= 0)
            {
                Splurge();
                inside = false;
                return;
            }
            
            percent = Mathf.Clamp01(percent-=.001f);
            time = Mathf.Max(0, time - Time.deltaTime);
        }
        else
        {
            percent = 0;
            time = 3;
        }
    }

    private void Splurge()
    {
        Instantiate(splurge, other.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TopDownMovement>().OnLeftButtonPress += OnLeftButtonPress;
        other.GetComponent<TopDownMovement>().OnRightButtonPress += OnRightButtonPress;

        if (!inside) this.other = other.transform;
        
        inside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<TopDownMovement>().OnLeftButtonPress -= OnLeftButtonPress;
        other.GetComponent<TopDownMovement>().OnRightButtonPress -= OnRightButtonPress;

        inside = false;
    }

    private void OnRightButtonPress(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            percent += .05f;
        }
    }

    private void OnLeftButtonPress(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            percent += .05f;
        }
    }
}