using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayGame : MonoBehaviour {

    public ReactionCollection delayReacitonCollection;
    
    public GameObject wantDestroy;

    private void DelayReactionStart()
    {
        delayReacitonCollection.InitIndex();
        delayReacitonCollection.React();

        if (wantDestroy != null)
            Destroy(wantDestroy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            DelayReactionStart();
        Debug.Log("Good");
    }
}
