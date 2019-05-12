using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingUntilClickMissionReaction : DelayedReaction
{
    private GameObject myCorotine;

    public ReactionCollection afterReaction;

    protected override void ImmediateReaction()
    {

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        MissionScript mission = GameObject.FindObjectOfType<MissionScript>();
        mission.isClicked = false;

        while (true)
        {
            yield return null;
            if(mission.isClicked == true)
            {
                afterReaction.InitAndReact();
                break;
            }
        }

        Destroy(myCorotine);
    }
}
