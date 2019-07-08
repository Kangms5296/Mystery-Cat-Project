#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GameEndReaction))]
public class GameEndReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Game End Reaction";
    }
}
