using System;
using System.Threading.Tasks;

public class Hammer : Tool
{
    public override void Interact(Interactable interactable)
    {
        HammerInteraction(interactable);
    }
    
    async void HammerInteraction(Interactable interactable)
    {
        print("hammerInteraction");
        await Task.Delay(TimeSpan.FromSeconds(5f));
    }
}
