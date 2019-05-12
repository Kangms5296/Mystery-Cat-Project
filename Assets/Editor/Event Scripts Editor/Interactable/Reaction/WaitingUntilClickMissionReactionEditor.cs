#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WaitingUntilClickMissionReaction))]
public class WaitingUntilClickMissionReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Click Mission Reaction";
    }
}
