using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionChangeReaction : DelayedReaction {

    // 지정할 Mission
    public string mission;

    public bool isHide = false;
    
    protected override void ImmediateReaction()
    {
        MissionScript missionUI = Transform.FindObjectOfType<MissionScript>();
        missionUI.SetMission(mission);

        if(!isHide)
            missionUI.OnClickUI();
    }
}
