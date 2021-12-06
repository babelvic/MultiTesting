using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, Interactable
{
    public Vector3 itemPosition;
    public Subpiece currentSubpiece;
    public ToolData acceptedToolData;
    
    public void SetSubpiece(Subpiece subpiece)
    {
        currentSubpiece = subpiece;
        subpiece.transform.position = itemPosition;
        subpiece.transform.parent = transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + itemPosition, Vector3.one);
    }

    public void Interact(InteractionManager interactionManager)
    {
        SetSubpiece(interactionManager.currentItem.GetComponent<Subpiece>());
    }
}
