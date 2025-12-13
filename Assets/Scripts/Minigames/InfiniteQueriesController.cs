using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteQueriesController : MonoBehaviour
{
    public string[] objects = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string currentObject;
    public TMP_Text commandText;

    public bool multiRound;
    public GameObject roundButton;

    public bool[] buzzerInputs;
    int players;

    private void Awake()
    {
        players = GameManager.instance.players;
    }

    private void Start()
    {
        multiRound = true;
    }

    public void StartRound()
    {
        //Get new command
        GetInstruction();
        roundButton.SetActive(false);
    }

    private void Update()
    {
        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

            if (buzzerInputs[i])
            {
                //Play buzz sound
                GameManager.instance.PlaySound(GameManager.instance.buzzSnd, 0.6f, true);

            }
        }
    }

    public void GetInstruction()
    {
        int index = Random.Range(0, objects.Length);
        currentObject = objects[index];

        commandText.text = currentObject;

        gameObject.SetActive(false);
    }
}
