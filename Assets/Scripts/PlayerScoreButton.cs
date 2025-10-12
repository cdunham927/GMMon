using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerScoreButton : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public int playerPlace;
    IPlayerOut pOut;
    public bool outIn;
    public int thisPlayer;
    [HideInInspector]
    public TMP_Text text;
    Image i;

    private void Start()
    {
        playerPlace = -1;
        text = GetComponentInChildren<TMP_Text>();
        pOut = FindObjectOfType<IPlayerOut>();
        i = GetComponent<Button>().GetComponent<Image>();
    }

    private void Update()
    {
        if (!outIn) text.text = "Player " + (thisPlayer + 1).ToString() + " - " + GameManager.instance.playerScores[thisPlayer].ToString();

        if (outIn)
        {
            string t = "";
            if (playerPlace != -1)
            {
                if (playerPlace == 1) t = (playerPlace + 1).ToString() + "st";
                else if (playerPlace == 2) t = (playerPlace + 1).ToString() + "nd";
                else if (playerPlace == 3) t = (playerPlace + 1).ToString() + "rd";
                else t = (playerPlace + 1).ToString() + "th";

                text.text = "Player " + (thisPlayer + 1).ToString() + " - " + t;
            }
            else {
                if (pOut.curPlace == 1) t = (pOut.curPlace + 1) + "st";
                else if (pOut.curPlace == 2) t = (pOut.curPlace + 1) + "nd";
                else if (pOut.curPlace == 3) t = (pOut.curPlace + 1) + "rd";
                else t = (pOut.curPlace).ToString() + "th";

                text.text = "Player " + (thisPlayer + 1).ToString() + " - " + t;
            }

            i.color = (playerPlace == -1) ? Color.white : Color.red;
        }
    }

    public void AddScore()
    {
        GameManager.instance.playerScores[thisPlayer]++;
    }

    public void SubScore()
    {
        if (GameManager.instance.playerScores[thisPlayer] > 0) GameManager.instance.playerScores[thisPlayer]--;
    }

    public void PlayerOut()
    {
        if (playerPlace == -1)
        {
            //Change color to red
            playerPlace = pOut.curPlace - 1;

            pOut.eliminatedPlayer++;
        }
    }

    public void PlayerIn()
    {
        if (playerPlace != -1)
        {
            //Change color to white
            playerPlace = -1;
            pOut.curPlace++;

            pOut.eliminatedPlayer--;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!outIn) AddScore();
            else PlayerOut();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Right Clicked");
            if (!outIn) SubScore();
            else PlayerIn();
        }
    }
}
