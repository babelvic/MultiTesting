using Photon.Pun;
using UnityEngine;

public class Piece : MonoBehaviour, Interactable, IUseObjectData
{
    private PieceData _pieceData;
    public ObjectData Data => _pieceData;
    public void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}