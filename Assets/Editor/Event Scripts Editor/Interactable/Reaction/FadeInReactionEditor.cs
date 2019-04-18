#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(FadeInReaction))]
public class FadeInReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "FadeIn Reaction";
    }
}
