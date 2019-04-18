using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistractorDisplayer : MonoBehaviour {

    public GameObject FirsttDistractorFrame;
    public GameObject SecondDistractorFrame;

    public Text firstDistractor;
    public Text secondDistractor;

    public Button firstButton;
    public Button secondButton;

    public void SetTextToFirstDistractor(string text)
    {
        firstDistractor.text = text;
    }

    public void SetTextToSecondDistractor(string text)
    {
        secondDistractor.text = text;
    }

	// Use this for initialization
	public void ShowDistractor()
    {
        FirsttDistractorFrame.SetActive(true);
        SecondDistractorFrame.SetActive(true);
    }

    public void HideDistractor()
    {
        FirsttDistractorFrame.SetActive(false);
        SecondDistractorFrame.SetActive(false);
    }

    public void DebugTest()
    {
        //Debug.Log("??");
    }
}
