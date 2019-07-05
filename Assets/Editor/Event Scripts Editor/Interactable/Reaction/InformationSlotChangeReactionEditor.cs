#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(InformationSlotChangeReaction))]
public class InformationSlotChangeReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Information Slot Change Reaction";
    }
}
