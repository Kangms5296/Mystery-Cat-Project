using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicUI : MonoBehaviour {

	public Vector3 parentVector;
	private Camera myCamera;
	private Vector3 myVector;
	private RectTransform myTransform;

	public void LateUpdate(){
		
		transform.position = myCamera.WorldToScreenPoint (myVector);
	}

	public void OnEnable(){
		myCamera = FindObjectOfType<Camera> ();

	
		transform.position = myCamera.WorldToScreenPoint (myVector);
		Debug.Log ("Create");
	}

	public void SetVector(Vector3 vector){
		parentVector = vector;
		myVector = new Vector3 (parentVector.x, parentVector.y + 2.0f, parentVector.z);
	}

	public void OnDestroy(){
		Debug.Log ("***");
		if(gameObject != null)
			if(FSLocator.uiContainer != null)
				FSLocator.uiContainer.DeleteObservation (gameObject);
	}
}
