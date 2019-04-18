using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uween;
using UnityEngine.UI;

enum CURRENTBUTTONDIR
{
	ZERO ,RIGHT, LEFT,
};

public class MoveImage : MonoBehaviour {

	public List<GameObject> ImageList;
	float currentMovePosition = 0f;

	CURRENTBUTTONDIR currentButtonDir;

	const float mainXPosition = 0f;
	const float mainWidth = 160f;
	const float mainHeight = 160f;
	const float mainImageWidth = 80;
	const float mainImageHeight = 100;
	const float imageDistance = 60f;
	const float imageMagnification = 1f / 4f;

	int[]imageHierachyIndex;

	// Use this for initialization
	void Start () {
		Debug.Log ("Init Move Image");

		currentButtonDir = CURRENTBUTTONDIR.ZERO;
		imageHierachyIndex = new int[ImageList.Count];

		for (int i = 0; i < ImageList.Count; i++) {
			imageHierachyIndex [i] = ImageList [i].transform.GetSiblingIndex ();
			Debug.Log (imageHierachyIndex [i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetMagnification(float x)
	{
		float magnification;

		if (x >= mainXPosition) {
			magnification = 1f - ((x - mainXPosition) * imageMagnification / imageDistance);
			return magnification;
		} else {
			magnification = 1f - ((mainXPosition - x) * imageMagnification / imageDistance);
			return magnification;
		}
	}

	public void MoveRight()
	{
		
		if (currentMovePosition > ImageList.Count - 2)
			return;

		if (currentButtonDir == CURRENTBUTTONDIR.LEFT) {
			currentMovePosition++;
		}

		currentButtonDir = CURRENTBUTTONDIR.RIGHT;

		//StartCoroutine ("ChangeImageScale");
		
		for (int i = 0; i < ImageList.Count; i++) {
			TweenX.Add(ImageList[i], 3f, (i - 1 - currentMovePosition) * 800f + mainXPosition).EaseOutElasticWith(20, 5f);
		}

		Debug.Log (currentMovePosition);

		currentMovePosition++;
		Debug.Log ("CurrentMovePosition" + currentMovePosition);
		//ImageList [(int)currentMovePosition].transform.SetAsLastSibling();

	}

	public void MoveLeft()
	{
		

		if (currentMovePosition < 0)
			return;

		if (currentButtonDir == CURRENTBUTTONDIR.RIGHT) {
			currentMovePosition--;
		}

		currentButtonDir = CURRENTBUTTONDIR.LEFT;

		//StartCoroutine ("ChangeImageScale");

		for (int i = 0; i < ImageList.Count; i++) {
			TweenX.Add(ImageList[i], 3f, (i - currentMovePosition) * 800f + mainXPosition).EaseOutElasticWith(20, 5f);
		}

		Debug.Log ("CurrentMovePosition" + currentMovePosition);
		//ImageList [(int)currentMovePosition].transform.SetAsLastSibling();

		currentMovePosition--;


	}

	IEnumerator ChangeImageScale()
	{
		Debug.Log ("Change");
		float changeTime = 0f;
		float changeFixTime = 3.0f;

		while(changeTime < changeFixTime){
			changeTime += Time.fixedDeltaTime;
			for (int i = 0; i < ImageList.Count; i++) {
				float magnification = GetMagnification (ImageList [i].transform.localPosition.x);
				//Debug.Log (ImageList [i].transform.localPosition.x + " " +  i + " " + magnification);
				ImageList [i].GetComponent<RectTransform>().sizeDelta = new Vector2 (mainWidth * magnification, mainHeight * magnification);
				ImageList [i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (mainImageWidth * magnification, mainImageHeight * magnification);
				Color tempColor = ImageList [i].GetComponent<Image> ().color;
				tempColor.a = 1f * magnification;
				ImageList [i].GetComponent<Image> ().color = tempColor;
			}
			yield return new WaitForFixedUpdate ();
		}
			
		yield return null;
	}
}
