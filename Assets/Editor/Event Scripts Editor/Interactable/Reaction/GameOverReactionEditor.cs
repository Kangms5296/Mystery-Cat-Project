#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;


[CustomEditor(typeof(GameOverReaction))]
public class GameOverReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "GameOver Reaction";
    }
}
