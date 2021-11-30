using System;
using System.Linq;
using UnityEngine;

public abstract class Tool : MonoBehaviour, Interactor
{
    public ToolData toolData;

    public abstract PieceData Interact(Interactable interactable);
}