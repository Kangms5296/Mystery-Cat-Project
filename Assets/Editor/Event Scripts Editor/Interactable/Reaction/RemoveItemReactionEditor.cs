#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(RemoveItemReaction))]
public class RemoveItemReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Remove Item Reaction";
    }
}
