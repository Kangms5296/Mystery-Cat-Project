using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour {

    public ReactionCollection clickReaction;

    public void OnClickWatch()
    {
        clickReaction.InitAndReact();
    }
}
