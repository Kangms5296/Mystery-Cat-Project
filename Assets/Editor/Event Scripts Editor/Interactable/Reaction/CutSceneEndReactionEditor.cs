#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(CutSceneEndReaction))]
public class CutSceneEndReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Cut Scene End Reaction";
    }
}