using UnityEngine;

public class ConditionCollectionText : ScriptableObject
{
    public string description;
    public Condition[] requiredConditions = new Condition[0];

    public string dialogue;
    public string characterName;
    public Sprite characterImage;

    public bool Check()
    {
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (!AllConditions.CheckCondition(requiredConditions[i]))
                return false;
        }
        return true;
    }

    public bool CheckAndReact()
    {
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (!AllConditions.CheckCondition(requiredConditions[i]))
                return false;
        }
        React();
        return true;
    }

    public void React()
    {
         FSLocator.textDisplayer.Say(dialogue, characterName);
         FSLocator.characterDisplayer.DrawImage(characterImage, characterName);
    }
}