using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkedMonobehaviour : MonoBehaviour
{
    private static int IDs;
    public int ID;

    public virtual void Start()
    {
        ID = IDs;
        IDs++;
    }
}
