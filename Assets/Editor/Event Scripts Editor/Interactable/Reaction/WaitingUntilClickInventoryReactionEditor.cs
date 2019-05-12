#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(WaitingUntilClickInventoryReaction))]
public class WaitingUntilClickInventoryReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Waiting Until Click Inventory Reaction";
    }
}
