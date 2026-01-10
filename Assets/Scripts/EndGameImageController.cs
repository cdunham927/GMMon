using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameImageController : MonoBehaviour
{
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color wrongSpotColor = Color.yellow;
    public Color highlightColor = Color.black;
    public Color unhighlightColor = Color.white;

    public Image answerImage;
    public Image highlightImage;
    public Image keyImage;

    public Sprite[] images;

    public int imageSlot = 0;

    private void Awake()
    {
        keyImage.sprite = images[imageSlot];
    }

    public void CorrectImage()
    {
        answerImage.color = correctColor;
    }

    public void IncorrectImage()
    {
        answerImage.color = incorrectColor;
    }

    public void WrongSpotImage()
    {
        answerImage.color = wrongSpotColor;
    }

    public void NextImage()
    {
        if (imageSlot < images.Length - 1)
            imageSlot++;
        else imageSlot = 0;

        keyImage.sprite = images[imageSlot];
    }

    public void PreviousImage()
    {
        if (imageSlot > 0)
            imageSlot--;
        else imageSlot = images.Length - 1;

        keyImage.sprite = images[imageSlot];
    }
}
