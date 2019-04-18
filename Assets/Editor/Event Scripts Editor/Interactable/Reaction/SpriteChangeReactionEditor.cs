#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(SpriteChangeReaction))]
public class SpriteChangeReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Sprite Change Reaction";
    }
}
