using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InteractionDictionary : ScriptableObject
{
    //singleton scriptable object
    public static InteractionDictionary Instance => instance ??= Resources.Load<InteractionDictionary>("Interaction Dictionary");
    private static InteractionDictionary instance;
    
    public List<Recipe> recipes = new List<Recipe>();
}

[Serializable]
public struct Recipe
{
    public ToolData tool;
    public SubpieceData subpiece;
    public PieceData result;
}
