using UnityEngine;

public class Tool : MonoBehaviour, IUseObjectData, Interactor
{
    private ToolData _toolData;
    public ObjectData Data => _toolData;
    public void Interact(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }
}