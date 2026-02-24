using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;
    public GameObject confirmMenu;
    public GameObject pauseMenu;
    public GameObject buttonParent;

    private void Awake()
    {
        Invoke("Begin", 0.1f);
    }

    private void Update()
    {
        pauseMenu.SetActive(GameManager.instance.paused);

        //if (Input.GetButtonDown("Pause") && GameManager.instance.paused)
        //{
        //    Debug.Log("Unpausing");
        //    ClosePauseMenu();
        //    GameManager.instance.Resume();
        //}
    }

    void Begin()
    {
        GameManager.instance.StartGame();


        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i].num = i;
        }
    }

    public void NextGame()
    {
        GameManager.instance.NextGame();
    }

    public void ResetGame()
    {
        GameManager.instance.ResetGame();
    }

    public void OpenConfirmMenu()
    {
        confirmMenu.SetActive(true);
    }

    public void CloseConfirmMenu()
    {
        confirmMenu.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        GameManager.instance.Pause();
    }
}
