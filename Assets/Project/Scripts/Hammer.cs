using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Hammer : Tool
{
    public override void Interact(Interactable interactable)
    {
        HammerInteraction(interactable);
    }
    
    async void HammerInteraction(Interactable interactable)
    {
        await Task.Delay(TimeSpan.FromSeconds(5f));
        
        
    }
}
