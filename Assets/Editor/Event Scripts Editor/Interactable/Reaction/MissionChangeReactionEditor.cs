#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(MissionChangeReaction))]
public class MissionChangeReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Mission Change Reaction";
    }
}
