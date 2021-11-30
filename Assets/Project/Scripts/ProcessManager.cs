using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    public static ProcessManager Instance => _instance ??= FindObjectOfType<ProcessManager>();
    private static ProcessManager _instance;
    
    public List<Recipe> currentAvailableRecipes;
    public Dictionary<(ToolData, SubpieceData), PieceData> RecipeDictionary = new Dictionary<(ToolData, SubpieceData), PieceData>();

    public static PieceData nullPieceData;

    private void Start()
    {
        currentAvailableRecipes = InteractionDictionary.Instance.recipes; // testeo
        RecipeDictionary = currentAvailableRecipes.ToDictionary(r => (r.tool, r.subpiece), r => r.result);
        nullPieceData = Resources.Load<PieceData>("NullPiece");
    }

    public PieceData Process(ToolData toolData, SubpieceData subpieceData)
    {
        if (RecipeDictionary.TryGetValue((toolData, subpieceData), out var result))
        {
            return result;
        }

        return nullPieceData;
    }
}
