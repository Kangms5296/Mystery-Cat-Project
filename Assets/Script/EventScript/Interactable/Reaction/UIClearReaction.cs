
public class UIClearReaction : DelayedReaction
{
    protected override void ImmediateReaction()
    {
        UICaching uiCaching = FindObjectOfType<UICaching>();
        uiCaching.ClearUI();
    }
}
