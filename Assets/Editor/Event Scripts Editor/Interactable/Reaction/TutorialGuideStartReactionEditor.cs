#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TutorialGuideStartReaction))]
public class TutorialGuideStartReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Tutorial Gudie Start Reaction";
    }
}
