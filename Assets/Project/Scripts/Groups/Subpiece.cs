using UnityEngine;

public class Subpiece : NetworkedInteractable
{
    public SubpieceData subPieceData;
    public override void Interact(InteractionManager interactionManager)
    {
        interactionManager.currentItem = gameObject;
        GetComponent<Collider>().enabled = false;
        transform.position = interactionManager.transform.position + transform.forward * 2;
        transform.parent = interactionManager.transform;
    }
}
