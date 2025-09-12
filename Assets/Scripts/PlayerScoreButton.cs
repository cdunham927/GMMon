using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerScoreButton : MonoBehaviour, IPointerClickHandler
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

    public void AddScore()
    {
        GameManager.instance.playerScores[thisPlayer]++;
    }

    public void SubScore()
    {
        if (GameManager.instance.playerScores[thisPlayer] > 0) GameManager.instance.playerScores[thisPlayer]--;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Left Clicked");
            AddScore();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Right Clicked");
            SubScore();
        }
    }
}
