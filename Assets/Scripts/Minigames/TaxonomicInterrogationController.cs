using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaxonomicInterrogationController : IPlayerOut
{
    public string[] categories = { "Colors", "Presidents", "Types of Birds", "Disney Movies", "Board Games" };

    public TMP_Text categoryText;
    public TMP_Text yurpusCategoryText;

    public int selectedPlayer;

    private void Start()
    {
        curPlace = GameManager.instance.players;
    }

    public void NextRound()
    {
        round++;
        if (eliminatedPlayer > 0) curPlace -= eliminatedPlayer;
        eliminatedPlayer = 0;

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
