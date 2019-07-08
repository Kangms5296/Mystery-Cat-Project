using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndReaction : DelayedReaction
{

    protected override void ImmediateReaction()
    {
        // app 종료
        Application.Quit();
    }
}
