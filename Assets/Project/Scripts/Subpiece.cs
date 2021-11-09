using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subpiece : MonoBehaviour, Interactable, IUseObjectData
{
    private SubpieceData _subPieceData;

    public ObjectData Data => _subPieceData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

}
