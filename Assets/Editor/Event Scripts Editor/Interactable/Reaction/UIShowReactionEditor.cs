#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(UIShowReaction))]
public class UIShowReactionEditor : ReactionEditor
{
    public GameObject[] UI;

    protected override string GetFoldoutLabel()
    {
        return "UI Show Reaction";
    }
}
