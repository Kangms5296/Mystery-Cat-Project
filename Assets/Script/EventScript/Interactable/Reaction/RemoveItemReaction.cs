using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemReaction : DelayedReaction
{
    public string itemID;

    private ContentScript playerInventory;

    public GameObject itemObject;

    protected override void ImmediateReaction()
    {
        playerInventory = GameObject.Find("ItemDisplayer").GetComponent<ContentScript>();

        playerInventory.RemoveItem(itemID);

        if (itemObject != null)
            Destroy(itemObject);
    }
}
