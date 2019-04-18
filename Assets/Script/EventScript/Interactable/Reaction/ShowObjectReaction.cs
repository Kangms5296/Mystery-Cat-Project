using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectReaction : DelayedReaction
{
    // Activated 하려는 오브젝트
    public List<GameObject> objects;

    protected override void ImmediateReaction()
    {
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
    }
}
