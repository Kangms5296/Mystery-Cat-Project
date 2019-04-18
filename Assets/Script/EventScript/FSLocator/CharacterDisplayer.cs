using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayer : MonoBehaviour {

    public Image characterBodyImage;

    public CanvasGroup characterCanvasGroup;

  
    
    public void HideImage()
    {
        characterBodyImage.color = new Color(1, 1, 1, 0);
    }


    public void DrawImage(Sprite sprite, string name)
    {
        characterBodyImage.sprite = sprite;

        if (sprite)
            characterBodyImage.color = new Color32(255, 255, 255, 255);
        else
            characterBodyImage.color = new Color32(255, 255, 255, 0);
    }
}
