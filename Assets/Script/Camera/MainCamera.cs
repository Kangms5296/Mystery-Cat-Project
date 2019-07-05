using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    /*
	private Transform Player;
	private Vector3 cameraDeapth;

	void Start(){
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		cameraDeapth = new Vector3 (0, 0, -60);
	}

	void LateUpdate(){
		transform.position = Player.position + cameraDeapth;
	}
    */
    
    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + new Vector3(0, 0, -60);
        //Vector2 temp = Vector2.Lerp(transform.position, player.position, 3 * Time.deltaTime);
        //transform.position = new Vector3(temp.x, temp.y, -300);
    }
}


