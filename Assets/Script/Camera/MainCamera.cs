using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	private Transform Player;
	private Vector3 cameraDeapth;

	void Start(){
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		cameraDeapth = new Vector3 (0, 0, -60);
	}

	void LateUpdate(){
		transform.position = Player.position + cameraDeapth;
	}
}


