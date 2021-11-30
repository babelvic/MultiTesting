using UnityEngine;

public abstract class Subpiece : MonoBehaviour, Interactable
{
    public SubpieceData subPieceData;

    public abstract void Interact(Interactor interactor);
}
