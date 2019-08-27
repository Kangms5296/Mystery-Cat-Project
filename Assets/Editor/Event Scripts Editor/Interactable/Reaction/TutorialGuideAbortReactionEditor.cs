#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TutorialGuideAbortReaction))]
public class TutorialGuideAbortReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Tutorial Gudie Abort Reaction";
    }
}
