using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuableActor : PickableActor
{
    public GameObject[] valuablePieces;

    protected override void MonoAwake()
    {
        base.MonoAwake();
        isDraggableOut = false;
    }

    public void Break()
    {
        isValid = false;
        rend.enabled = false;
        foreach (GameObject o in valuablePieces)
        {
            o.SetActive(true);
        }
    }
}
