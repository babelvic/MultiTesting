using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Object", menuName = "LevelObject", order = 0)]
public class levelObjectData : ScriptableObject
{
    public string name;
    public GameObject model;
}
