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
    public GameObject endGameObj;

    public bool multiRound = true;

    //Audio
    AudioSource src;
    public AudioClip buzzSnd;

    public int curNumMinigame = 0;

    public SelectableGame[] gamesButtons;

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

        src = GetComponent<AudioSource>();
        curNumMinigame = 0;


        //resize games to maxgame size

    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    int HasMinigame(string g)
    {
        for (int i = 0; i < games.Count; i++)
        {
            if (games[i] == g)
            {
                return i;
            }
        }
        return -1;
    }

    //void RemoveMinigame(int n)
    //{
    //    if (n == curNumMinigame - 1)
    //    {
    //        games[n] = "";
    //        curNumMinigame--;
    //        return;
    //    }
    //
    //    for (int i = n; i < curNumMinigame - 1; i++)
    //    {
    //        gamesButtons[n + 1].curNum = n;
    //        games[n + 1] = games[n];
    //    }
    //
    //    curNumMinigame--;
    //}

    public void ResetMinigames()
    {
        //for (int i = 0; i < maxGames; i++)
        //{
        //    games[i] = "";
        //}

        for (int i = 0; i < gamesButtons.Length; i++)
        {
            gamesButtons[i].curNum = -1;
        }

        games.Clear();
        curNumMinigame = 0;
    }

    public void AddMinigame(string game)
    {
        //if (curNumMinigame < maxGames && HasMinigame(game) == -1)
        //{
        //    games[curNumMinigame] = game;
        //    curNumMinigame++;
        //}
        if (!games.Contains(game))
        {
            //curNumMinigame++;
            games.Add(game);
        }
    }

    //public void SubMinigame(string game)
    //{
    //    int h = HasMinigame(game);
    //
    //    if (h != -1)
    //    {
    //        RemoveMinigame(h);
    //    }
    //}

    public void Continue()
    {
        if (curNumMinigame > 0)
        {
            //ListExtensions.Shuffle(games);

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
        //PlayerScoreButton[] scoreButtons = FindObjectOfType<EndGameFunctions>().playerScores;
        //PlayerScore[] scoreText = FindObjectOfType<PlayerFunctions>().playerScoresText;

        //foreach (PlayerScoreButton g in scoreButtons)
        //{
        //    g.gameObject.SetActive(false);
        //}
        //
        //for (int i = 0; i < players; i++)
        //{
        //    scoreButtons[i].thisPlayer = i;
        //    scoreButtons[i].gameObject.SetActive(true);
        //
        //    //scoreText[i].thisPlayer = i;
        //    scoreText[i].gameObject.SetActive(true);
        //}

        GameObject o = Instantiate(endGameObj);
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

        if (curNumMinigame <= 0) return;

        ResetMinigames();
    }

    public void NextGame()
    {
        if (curNumMinigame > 0)
        {
            //Set all scores to 0 and set players to the original amount
            for (int i = 0; i < players; i++)
            {
                playerScores[i] = 0;
            }

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

    public void PlaySound(AudioClip c, float vol = 1f)
    {
        float startVol = src.volume;
        src.volume = vol;
        src.PlayOneShot(c);
        src.volume = startVol;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
