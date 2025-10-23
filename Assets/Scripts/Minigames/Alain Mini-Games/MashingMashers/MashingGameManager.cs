using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class MashingGameManager : MonoBehaviour
{
    // -- UI Elements and Game Settings --
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game Settings")]
    [SerializeField] private float mashingDuration = 60f;
    [SerializeField] private float maxSwitchInterval = 10f;
    [SerializeField] private float minSwitchInterval = 2f;
    [SerializeField] private int wrongButtonPenalty = 1;

    // -- Private Game State Variables --
    private enum GameState { WaitingToStart, Mashing, GameOver }
    private GameState currentState;
    private int correctButtonIndex;
    private string[] buttonNames = { "First", "Second", "Third", "Fourth" };
    private float gameTimer;
    private float buttonSwitchTimer;
    private int score;
    private bool canStartGame = false;

    void Start()
    {
        currentState = GameState.WaitingToStart;
        instructionText.text = "Press any button to start the game!";
        timerText.text = "";
        scoreText.text = "";

        Invoke("AllowGameToStart", 0.5f);
    }

    private void AllowGameToStart()
    {
        canStartGame = true;
    }

    // --- Input Action Handlers ---
    public void OnButton() { HandleMash(0); }
    public void OnButton2() { HandleMash(1); }
    public void OnButton3() { HandleMash(2); }
    public void OnButton4() { HandleMash(3); }

    private void HandleMash(int buttonIndex)
    {
        if (currentState == GameState.WaitingToStart && canStartGame)
        {
            StartGame();
            return;
        }

        if (currentState == GameState.Mashing)
        {
            if (buttonIndex == correctButtonIndex) { score++; }
            else { score = Mathf.Max(0, score - wrongButtonPenalty); }
            scoreText.text = "Score: " + score;
        }
    }

    private void StartGame()
    {
        currentState = GameState.Mashing;
        score = 0;
        gameTimer = mashingDuration;

        SwitchCorrectButton();
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (currentState == GameState.Mashing)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = $"Time: {gameTimer:F2}";
            buttonSwitchTimer -= Time.deltaTime;
            if (buttonSwitchTimer <= 0)
            {
                SwitchCorrectButton();
            }
            if (gameTimer <= 0)
            {
                EndGame();
            }
        }
    }

    private void SwitchCorrectButton()
    {
        int oldButtonIndex = correctButtonIndex;
        while (correctButtonIndex == oldButtonIndex)
        {
            correctButtonIndex = Random.Range(0, 4);
        }
        instructionText.text = $"Mash the {buttonNames[correctButtonIndex]} button!";

        float gameProgress = (mashingDuration - gameTimer) / mashingDuration;
        buttonSwitchTimer = Mathf.Lerp(maxSwitchInterval, minSwitchInterval, gameProgress);
    }

    private void EndGame()
    {
        currentState = GameState.GameOver;
        timerText.text = "Time: 0.00";
        instructionText.text = $"Game Over! Final Score: {score}\nPress any button to play again.";
        canStartGame = false;
        Invoke("AllowRestart", 2f);
    }

    private void AllowRestart()
    {
        currentState = GameState.WaitingToStart;
        canStartGame = true;
    }
}