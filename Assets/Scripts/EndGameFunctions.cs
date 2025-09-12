using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;

    void Start()
    {
        GameManager.instance.ShowEndgame();
    }

    public void QuitToMenu()
    {
        GameManager.instance.ResetGame();
    }
}
