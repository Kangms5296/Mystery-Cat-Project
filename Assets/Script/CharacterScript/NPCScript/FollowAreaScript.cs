using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

public class FollowAreaScript : MonoBehaviour {

    public ReactionCollection endReacitonCollection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어를 잡으면..
        if (collision.tag == "Player")
        {
            // 따라다니는걸 멈추고..
            transform.parent.GetComponent<FollowTarget>().enabled = false;
            transform.parent.GetComponent<PolyNavAgent>().Stop();
            transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Ending Reaction 호출
            endReacitonCollection.InitAndReact();
        }
    }
}
