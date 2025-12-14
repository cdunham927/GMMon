using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlawInYourLogicController : MonoBehaviour
{
    public string[] principles = { "Statement must be a question", "Statement must have 8 words", "Statement must have the conjunction 'but" };
    public string[] hints = { "Are you smarter than a biggusglorbo?", "The fractal trees on Yagrina 5 are purple.", "You are dumb, but perfect for being pets." };
    
    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

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
        multiRound = false;
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

    public void StartRound()
    {
        //Get new command
        GetPrinciple();
        roundButton.SetActive(false);
    }

    public void GetPrinciple()
    {
        int index = Random.Range(0, principles.Length);
        
        commandText.text = principles[index];
        yurpusCommandText.text = "Hint: " + hints[index];

        gameObject.SetActive(false);
    }
}

