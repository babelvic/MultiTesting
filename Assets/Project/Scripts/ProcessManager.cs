using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    public static List<ProcessType> processTypeList;

    public static PieceData Process(ToolData toolData, SubpieceData subpieceData)
    {
        return processTypeList.First(p => (p.ToolData == toolData && p.SubpiceData == subpieceData)).PieceData;
    }
}

[System.Serializable]
public struct ProcessType
{
    public ToolData ToolData;
    public SubpieceData SubpiceData;
    public PieceData PieceData;
}
