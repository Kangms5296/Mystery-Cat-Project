#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(InstantDataChangeReaction))]
public class InstantDataChangeReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Instant Data Change Reaction";
    }
}