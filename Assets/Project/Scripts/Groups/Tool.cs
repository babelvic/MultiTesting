using System;
using System.Linq;
using UnityEngine;

public class Tool : NetworkedInteractable, Interactor
{
    public ToolData toolData;

    public PieceData Interact(Interactable interactable)
    {
        return ProcessManager.Instance.Process(toolData, (interactable as Subpiece)?.subPieceData);
    }

    public override void Interact(InteractionManager interactionManager)
    {
        interactionManager.currentTool = gameObject;
        GetComponent<Collider>().enabled = false;
        transform.position = interactionManager.transform.position + Vector3.up * 2;
        transform.parent = interactionManager.transform;
    }
}