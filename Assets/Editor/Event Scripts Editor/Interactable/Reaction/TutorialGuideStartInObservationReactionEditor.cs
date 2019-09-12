#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TutorialGuideStartInObservationReaction))]
public class TutorialGuideStartInObservationReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Tutorial Gudie Start In Observation Reaction";
    }
}
