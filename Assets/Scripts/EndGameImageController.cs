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
}
