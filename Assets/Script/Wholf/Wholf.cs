using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wholf : MonoBehaviour {
	public Interactable interactable;

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			if (interactable) {
                col.GetComponent<BoxCollider2D>().enabled = false;
				col.GetComponent<NewPlayer> ().Stop ();
				interactable.Interact ();
			}
		}
	}
}
