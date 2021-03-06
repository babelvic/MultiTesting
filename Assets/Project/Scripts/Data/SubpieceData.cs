using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Subpiece Data", menuName = "SubpieceData", order = 0)]
public class SubpieceData : ObjectData
{
    Dictionary<ToolData, PieceData> toolToPiece = new Dictionary<ToolData, PieceData>();
}
