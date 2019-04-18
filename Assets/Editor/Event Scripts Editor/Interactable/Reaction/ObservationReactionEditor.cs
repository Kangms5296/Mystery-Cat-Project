#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(ObservationReaction))]
public class ObservationReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Observation Reaction";
    }
}
