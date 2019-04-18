#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(EventCallbackReaction))]
public class EventCallbackReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Event Callback Reaction";
    }
}
