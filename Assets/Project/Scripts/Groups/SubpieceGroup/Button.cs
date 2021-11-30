using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Subpiece
{
    public override void Interact(Interactor interactor)
    {
        print("ButtonInteract");
    }
}
