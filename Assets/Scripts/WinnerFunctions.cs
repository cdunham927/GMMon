using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerFunctions : MonoBehaviour
{
    public PlayerWin playerWinViewPrefab;
    PlayerWin playerWinView;

    public void SelectWinner(int val)
    {
        playerWinView = Instantiate(playerWinViewPrefab);

        playerWinView.SetText("Player " + val.ToString() + " Wins!");
        playerWinView.gameObject.SetActive(true);
    }
}
