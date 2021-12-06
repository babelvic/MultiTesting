public interface Interactable
{
    public void Interact(InteractionManager interactionManager); // E
}

public interface Interactor
{
    public abstract PieceData Interact(Interactable interactable); // I
}