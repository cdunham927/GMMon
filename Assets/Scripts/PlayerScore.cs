using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    MenuFunctions menu;
    public int num;
    public PlayerScoreButton parent;
    TMP_Text text;
    Image i;
    public Image catImpImage;

    private void Awake()
    {
        menu = FindObjectOfType<MenuFunctions>();
        text = GetComponentInChildren<TMP_Text>();
        i = GetComponentInChildren<Image>();

        parent = menu.playerScores[num];
    }


    private void Update()
    {
        if (parent != null) text.text = parent.text.text;

        if (parent.outIn) i.color = (parent.playerPlace == -1) ? Color.white : Color.red;
    }
}
