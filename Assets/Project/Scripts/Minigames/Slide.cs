using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    private Rigidbody rb;
    private TopDownMovement _topDownMovement;

    private void Start()
    {
        _topDownMovement = GetComponent<TopDownMovement>();
        rb = GetComponent<Rigidbody>();
        _topDownMovement.enabled = false;
    }

    private void Update()
    {
        rb.velocity = 2 * transform.forward * _topDownMovement.speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Floor")
        {
            _topDownMovement.enabled = true;
            Destroy(this);
        }
    }
}
