using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public PlayerScoreButton parent;
    TMP_Text text;
    Image i;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        i = GetComponentInChildren<Image>();
    }


    private void Update()
    {
        if (parent) text.text = parent.text.text;

        if (parent.outIn) i.color = (parent.playerPlace == -1) ? Color.white : Color.red;
    }
}
