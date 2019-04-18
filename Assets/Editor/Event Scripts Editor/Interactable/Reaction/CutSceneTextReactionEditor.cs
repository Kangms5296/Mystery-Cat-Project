using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(CutSceneTextReaction))]
public class CutSceneTextReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Cut Scene Text Reaction Reaction";
    }
}
