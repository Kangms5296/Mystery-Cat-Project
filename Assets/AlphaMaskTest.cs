using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlphaMaskTest : MonoBehaviour {

    public RectTransform t;

    [Range(0.001f, 30)]
    public float size = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t.localScale = new Vector3(size, size, 1);
	}
}
