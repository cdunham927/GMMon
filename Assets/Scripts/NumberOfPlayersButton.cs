using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class NumberOfPlayersButton : MonoBehaviour, IPointerClickHandler
{
    TMP_Text text;

    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.text = "Players - " + GameManager.instance.players.ToString();
    }

    void AddPlayer()
    {
        if (GameManager.instance.players < 10)
        {
            GameManager.instance.players++;
        }
        text.text = "Players - " + GameManager.instance.players.ToString();
    }

    void RemovePlayer()
    {
        if (GameManager.instance.players > 2) 
        {
            GameManager.instance.players--;
        }
        text.text = "Players - " + GameManager.instance.players.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Left Clicked");
            AddPlayer();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Right Clicked");
            RemovePlayer();
        }
    }
}