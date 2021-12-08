using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : NetworkedInteractable, Interactable
{
    public Vector3 itemPosition;
    public Subpiece currentSubpiece;
    public ToolData acceptedToolData;
    
    public void SetSubpiece(Subpiece subpiece)
    {
        currentSubpiece = subpiece;
        subpiece.transform.position = itemPosition + transform.position;
        subpiece.transform.parent = transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + itemPosition, Vector3.one);
    }

    public override void Interact(InteractionManager interactionManager)
    {
        SetSubpiece(interactionManager.currentItem.GetComponent<Subpiece>()); // add checks for current item not being a subpiece or being null
    }
}
