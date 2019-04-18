using UnityEngine;

public class ConditionCollection : ScriptableObject
{
    public string description;
    public Condition[] requiredConditions = new Condition[0];
    public ReactionCollection reactionCollection;
    private bool isFirst = false;

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
            if (!AllConditions.CheckCondition (requiredConditions[i]))
                return false;
        }
        if (reactionCollection)
            InvokeConditionReaction();

        return true;
    }



    public void InvokeConditionReaction()
    {
        isFirst = false;

        if (!isFirst)
        {
            isFirst = true;

			reactionCollection.InitIndex ();
			reactionCollection.React();
            /*if (!FSLocator.controlManager.EventIsPlaying)
                ;*/
        }
    }

    public void InvokeConditionReactionAndCallback()
    {
        /*FSLocator.controlManager.m_Button.onClick.RemoveAllListeners();
        FSLocator.controlManager.m_Button.onClick.AddListener(delegate { reactionCollection.React(); });
        FSLocator.controlManager.m_Button.onClick.Invoke();*/
    }

    public void SkipReaction()
	{
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (!AllConditions.CheckCondition(requiredConditions[i]))
                return;
        }
        if (reactionCollection)
            reactionCollection.Skip();

    }
}