using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splurge : MonoBehaviour
{
    private bool beenInsideOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (beenInsideOnce)
        {
            other.gameObject.AddComponent<Slide>();
            Destroy(gameObject);
        }

        beenInsideOnce = true;
    }
}