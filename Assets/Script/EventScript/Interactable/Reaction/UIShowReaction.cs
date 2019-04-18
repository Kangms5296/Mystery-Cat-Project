using UnityEngine;

public class UIShowReaction : DelayedReaction
{
    private UICaching uiCaching;

    protected override void ImmediateReaction()
    {
        uiCaching = FindObjectOfType<UICaching>();

        foreach (var ui in uiCaching.GetUI())
            ui.gameObject.SetActive(true);
    }
}