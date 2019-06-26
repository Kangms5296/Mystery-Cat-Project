#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WaitingUntilMixReaction))]
public class WaitingUntilMixReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Mix Reaction";
    }
}
