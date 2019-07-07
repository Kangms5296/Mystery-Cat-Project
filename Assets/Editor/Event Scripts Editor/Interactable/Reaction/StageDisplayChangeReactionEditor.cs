#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(StageDisplayChangeReaction))]
public class StageDisplayChangeReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Stage Display Change Reaction";
    }
}