using System;
using System.Linq;
using UnityEngine;

public class Tool : NetworkedMonobehaviour, Interactor
{
    public ToolData toolData;

    public PieceData Interact(Interactable interactable)
    {
        return ProcessManager.Instance.Process(toolData, (interactable as Subpiece)?.subPieceData);
    }
}