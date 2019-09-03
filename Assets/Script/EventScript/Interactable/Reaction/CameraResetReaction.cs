using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResetReaction : DelayedReaction
{

    protected override void ImmediateReaction()
    {
        MainCamera camera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        camera.SetNewTarget(camera.player);
    }
}
