using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlphaMaskTest : MonoBehaviour {

    public RectTransform mask;

    [Range(0.001f, 50)]
    public float size = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mask.localScale = new Vector3(size, size, 1);
	}
}
