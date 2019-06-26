#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WaitingUntilClickDocuInfoReaction))]
public class WaitingUntilClickDocuInfoReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Click DocuInfo Reaction";
    }
}
