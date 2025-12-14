using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaxonomicInterrogationController : IPlayerOut
{
    public string[] categories = { "Colors", "Presidents", "Types of Birds", "Disney Movies", "Board Games" };

    public TMP_Text categoryText;
    public TMP_Text yurpusCategoryText;

    public int selectedPlayer;

    public Image dirImg;
    public Sprite lArrow;
    public Sprite rArrow;

    public bool multiRound;

    public bool[] buzzerInputs;
    int players;

    private void Awake()
    {
        players = GameManager.instance.players;
    }

    private void Start()
    {
        multiRound = true;
        curPlace = GameManager.instance.players;
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

    public void NextRound()
    {
        round++;
        if (eliminatedPlayer > 0) curPlace -= eliminatedPlayer;
        eliminatedPlayer = 0;

        dirImg.sprite = (Random.value > 0.5f) ? lArrow : rArrow;

        if (curPlace <= 1)
        {
            //playerHudText.SetActive(false);
            GameManager.instance.ShowEndgame();
        }

        //Get new command
        GetRandomPlayer();
        GetCategory();
    }

    public void GetRandomPlayer()
    {
        selectedPlayer = Random.Range(0, GameManager.instance.players);
    }

    public void GetCategory()
    {
        int index = Random.Range(0, categories.Length);

        categoryText.text = "Category: " + categories[index] + "\nStarting Player: " + selectedPlayer.ToString();
        yurpusCategoryText.text = "Category: " + categories[index];
    }
}
