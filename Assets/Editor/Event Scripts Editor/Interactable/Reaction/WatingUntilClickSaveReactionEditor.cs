#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WatingUntilClickSaveReaction))]
public class WatingUntilClickSaveReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Click Save Reaction";
    }
}
