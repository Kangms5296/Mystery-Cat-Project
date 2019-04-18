using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(CutSceneShowTextReaction))]
public class CutSceneTextShowReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Cut Scene Show Text Reaction Reaction";
    }
}
