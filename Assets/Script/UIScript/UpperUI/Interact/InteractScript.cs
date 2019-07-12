using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {


    // 현재 상호작용 거리에 있는 오브젝트
    [HideInInspector]
    public GameObject interactObject;

    // 현재 상호작용 거리에 있는 오브젝트의 상호작용 내용.
    [HideInInspector]
    public Interactable interactable;
    
    
    // ====================================================================== public function =========================================================================

     public void OnClickInteraction()
    {
        if (interactable != null)
            interactable.Interact();
    }
}
