using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Piece Data", menuName = "PieceData", order = 0)]
public class PieceData : ObjectData
{
    public Mesh pieceMesh;
    public Material pieceMat;
}
