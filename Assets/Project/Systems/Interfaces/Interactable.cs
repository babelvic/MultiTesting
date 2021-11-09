public interface Interactable
{
    public void Interact(Interactor interactor);
}

public interface Interactor
{
    public void Interact(Interactable interactable);
}