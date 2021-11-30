using System;
using System.Threading.Tasks;

public class Hammer : Tool
{
    public override PieceData Interact(Interactable interactable)
    {
        return HammerInteraction(interactable);
    }
    
    public PieceData HammerInteraction(Interactable interactable)
    {
        return ProcessManager.Instance.Process(toolData, (interactable as Subpiece)?.subPieceData);
    }
}
