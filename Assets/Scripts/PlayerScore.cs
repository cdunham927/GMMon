using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int thisPlayer;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        text.text = "Player " + (thisPlayer + 1).ToString() + " - " + GameManager.instance.playerScores[thisPlayer].ToString();
    }
}
