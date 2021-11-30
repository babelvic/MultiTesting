using Photon.Pun;
using UnityEngine;

public abstract class Piece : MonoBehaviour, Interactable
{
    public PieceData pieceData;

    public abstract void Interact(Interactor interactor);
}