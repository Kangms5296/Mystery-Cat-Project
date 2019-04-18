using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float time_cnt;

	// Use this for initialization
	void Start () {
        time_cnt = 0;

    }
	
	// Update is called once per frame
	void Update () {
        time_cnt += Time.deltaTime;
	}
}
