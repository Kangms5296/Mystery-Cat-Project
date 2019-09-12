#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TutorialGuideStartInMixReaction))]
public class TutorialGuideStartInMixReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Tutorial Gudie Start In Mix Reaction";
    }
}
