#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(CutSceneStartReaction))]
public class CutSceneStartReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Cut Scene Start Reaction";
    }
}