using UnityEngine;

public class UIAddReaction : DelayedReaction
{
    public GameObject ui;

    protected override void ImmediateReaction()
    {
        UICaching uiCaching = FindObjectOfType<UICaching>();
        uiCaching.AddUI(ui);
    }
}
