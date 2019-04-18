using UnityEngine;

public class UIHideReaction : DelayedReaction
{
    private UICaching uiCaching;

    protected override void ImmediateReaction()
    {
        //uiCaching = GameObject.Find("UI_Canvas").GetComponent<UICaching>();
        uiCaching = FindObjectOfType<UICaching>();

        foreach (var ui in uiCaching.GetUI())
            ui.gameObject.SetActive(false);
    }
}
