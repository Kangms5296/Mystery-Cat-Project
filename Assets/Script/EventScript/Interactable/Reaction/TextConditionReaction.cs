using UnityEngine;


public class TextConditionReaction : Reaction
{
    public ConditionCollectionText[] conditionCollectionsText = new ConditionCollectionText[0];

    protected override void ImmediateReaction()
    {
        for (int i = 0; i < conditionCollectionsText.Length; i++)
        {
            if (conditionCollectionsText[i].Check())
            {
                conditionCollectionsText[i].React();
                //FSLocator.controlManager.reactPreCondition = true;
                return;
            }
        }
        //FSLocator.controlManager.reactPreCondition = false;
    }
}