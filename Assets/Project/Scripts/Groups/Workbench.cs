using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : NetworkedMonoBehaviour, IObjectDropable
{
    public Vector3 itemPosition;
    public SubpieceData currentSubpiece;
    public ToolData acceptedToolData;
    
    public void SetSubpiece(Item subpiece)
    {
        currentSubpiece = subpiece.itemData as SubpieceData;
        subpiece.transform.position = itemPosition + transform.position;
        subpiece.transform.parent = transform;
    }
    //
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireCube(transform.position + itemPosition, Vector3.one);
    // }
}
