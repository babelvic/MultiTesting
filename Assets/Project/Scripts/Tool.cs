using System.Linq;
using UnityEngine;

public abstract class Tool : MonoBehaviour, IUseObjectData, Interactor
{
    private ToolData _toolData;
    public ObjectData Data => _toolData;
    public abstract void Interact(Interactable interactable);

    public void DetectInteraction()
    {
        Collider[] objects = Physics.OverlapSphere(this.transform.position, 5f, 1 << LayerMask.NameToLayer("Subpiece"));
        var subPiceDetected = objects.FirstOrDefault(o => o.GetComponent<Subpiece>());
        if (subPiceDetected is not null) subPiceDetected.GetComponent<Subpiece>().Interact(this);
    }
}