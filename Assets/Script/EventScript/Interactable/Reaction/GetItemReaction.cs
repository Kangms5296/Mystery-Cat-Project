using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemReaction : DelayedReaction
{
    public string itemID;

    private ContentScript playerInventory;

	public GameObject itemObject;

    protected override void ImmediateReaction()
    {
        playerInventory = GameObject.Find("ItemDisplayer").GetComponent<ContentScript>();
		//During Set Item Number
        playerInventory.GetItem(itemID);

		if (itemObject != null)
			Destroy (itemObject);
    }
}
