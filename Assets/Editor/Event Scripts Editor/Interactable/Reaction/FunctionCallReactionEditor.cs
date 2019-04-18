#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;


[CustomEditor(typeof(FunctionCallReaction))]
public class FunctionCallReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Function Call Reaction";
    }
}
