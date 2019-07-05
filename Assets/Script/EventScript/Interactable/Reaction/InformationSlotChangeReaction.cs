using UnityEngine;
using UnityEngine.UI;


public class InformationSlotChangeReaction : DelayedReaction
{
    public InformationSlot slot;
    public InformationClue clue;

    public string infoName;
    [TextArea] public string infoExp;
    public Sprite infoImage;
    
    protected override void ImmediateReaction()
    {
        if(slot != null)
        {
            slot.informationName = infoName;
            slot.informationExp = infoExp;
            slot.informationImage = infoImage;
        }

        if(clue != null)
        {
            clue.informationName = infoName;
            clue.informationExp = infoExp;
            clue.informationImage = infoImage;

            clue.transform.Find("Known").GetComponent<Image>().sprite = infoImage;

        }
    }
}
