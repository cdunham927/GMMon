using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;

    public EndGameImageController[] keycardControllers;
    public int curKeycard;

    public void NextImage(Image i)
    {
        //i.sprite = 
    }

    public void ConfirmGuess(int p)
    {

    }

    void Start()
    {
        GameManager.instance.ShowEndgame();
        GameManager.instance.endgame = this;
    }

    public void QuitToMenu()
    {
        GameManager.instance.ResetGame();
    }
}
