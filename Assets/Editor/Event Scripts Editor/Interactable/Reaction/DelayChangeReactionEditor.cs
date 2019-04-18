#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(DelayChangeSceneReaction))]
public class DelayChangeSceneReactionEditor : ReactionEditor
{
    
    protected override void Init()
    {

    }

    protected override string GetFoldoutLabel()
    {
        return "Delay Change Scene Reaction";
    }
}
