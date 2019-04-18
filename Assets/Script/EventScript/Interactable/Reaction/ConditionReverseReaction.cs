public class ConditionReverseReaction : DelayedReaction
{
    public Condition condition;

    protected override void SpecificInit()
    {

    }

    protected override void ImmediateReaction()
    {
        condition.satisfied = false;
    }
}
