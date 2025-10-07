using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int players = 2;
    public GameObject mainMenu;
    public GameObject selectMenu;
    public TMP_Text playerNumText;

    public int[] playerScores;
    public List<string> games;
    public int maxGames;

    public bool paused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void AddMinigame(string game)
    {
        if (games.Count < maxGames && !games.Contains(game))
        {
            games.Add(game);
        }
    }

    public void SubMinigame(string game)
    {
        if (games.Contains(game)) games.Remove(game);
    }

    public void Continue()
    {
        if (games.Count > 0)
        {
            ListExtensions.Shuffle(games);

            //Load first scene
            SceneManager.LoadScene(games[0]);
            games.Remove(games[0]);
        }
    }

    public void StartGame()
    {
        PlayerScoreButton[] scoreButtons = FindObjectOfType<MenuFunctions>().playerScores;
        PlayerScore[] scoreText = FindObjectOfType<PlayerFunctions>().playerScoresText;

        foreach (PlayerScoreButton g in scoreButtons)
        {
            g.gameObject.SetActive(false);
        }

        for (int i = 0; i < players; i++)
        {
            scoreButtons[i].thisPlayer = i;
            scoreButtons[i].gameObject.SetActive(true);

            //scoreText[i].thisPlayer = i;
            scoreText[i].gameObject.SetActive(true);
        }
    }

    public void ShowEndgame()
    {
        PlayerScoreButton[] scoreButtons = FindObjectOfType<EndGameFunctions>().playerScores;
        PlayerScore[] scoreText = FindObjectOfType<PlayerFunctions>().playerScoresText;

        foreach (PlayerScoreButton g in scoreButtons)
        {
            g.gameObject.SetActive(false);
        }

        for (int i = 0; i < players; i++)
        {
            scoreButtons[i].thisPlayer = i;
            scoreButtons[i].gameObject.SetActive(true);

            //scoreText[i].thisPlayer = i;
            scoreText[i].gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        if (paused) paused = false;
        else paused = true;
    }

    public void ResetGame()
    {
        //Set all scores to 0 and set players to the original amount
        for (int i = 0; i < players; i++)
        {
            playerScores[i] = 0;
        }

        players = 2;

        //Then load the main menu
        SceneManager.LoadScene(0);
    }

    public void NextGame()
    {
        if (games.Count > 0)
        {
            //Load first scene
            SceneManager.LoadScene(games[0]);
            games.Remove(games[0]);
        }
        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void SelectGamesButton()
    {
        System.Array.Resize(ref playerScores, players);

        selectMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
