#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WaitingUntilClickDocuCharReaction))]
public class WaitingUntilClickDocuCharReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Click DocuChar Reaction";
    }
}
