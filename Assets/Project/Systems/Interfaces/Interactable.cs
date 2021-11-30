public interface Interactable
{
    // public void Interact(Interactor interactor);
}

public interface Interactor
{
    public abstract PieceData Interact(Interactable interactable);
}