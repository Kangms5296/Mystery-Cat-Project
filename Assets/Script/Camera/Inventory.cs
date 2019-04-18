using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData{
	public int id;
	public int sprite;

}

public class Inventory : MonoBehaviour {

	public List<int> idList;
	public List<Sprite> spriteList;

	public Image image;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Init(){

	}

	public void GetItem(int id){
		for (int i = 0; i < idList.Count; i++) {
			if (idList [i] == id) {
				image.sprite = spriteList [i];
			}
		}
	}
}
