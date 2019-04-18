using UnityEngine;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public Transform interactionLocation;
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;

	public bool isSkip = false;

	public void Start()
	{

	}

    // Event Trigger 딱 한번만 실행되는 시작함수 (조건에 맞으면, Start)
    public void Interact ()
    {
        for (int i = 0; i < conditionCollections.Length; i++)
        {			
			if (conditionCollections [i].CheckAndReact ()) {
				return;
			}
        }
        
        InvokeDefaultReaction();
    }

    public void InvokeDefaultReaction()
    {
 
		defaultReactionCollection.InitIndex();
		defaultReactionCollection.React();
        
    }

}
	