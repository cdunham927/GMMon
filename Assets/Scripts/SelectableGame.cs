using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SelectableGame : MonoBehaviour, IPointerClickHandler
{
    public string gameName;
    Button b;
    private Color originalColor;
    public TMP_Text text;
    public string startString;
    public int curNum = -1;

    void Awake()
    {
        b = GetComponent<Button>();
        originalColor = b.image.color;
        startString = text.text;
    }

    void Update()
    {
        if (curNum == -1)
        {
            b.image.color = originalColor;
            text.text = startString;
        }
        else
        {
            b.image.color = Color.yellow;
            text.text = curNum.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (curNum == -1 && GameManager.instance.curNumMinigame < GameManager.instance.maxGames)
            {
                AddGame();
                Highlight();
            }
        }
        //else if (eventData.button == PointerEventData.InputButton.Right)
        //{
        //    SubGame();
        //    Unhighlight();
        //}
    }

    void AddGame()
    {
        GameManager.instance.curNumMinigame++;
        GameManager.instance.AddMinigame(gameName);
    }

    //void SubGame()
    //{
    //    GameManager.instance.SubMinigame(gameName);
    //}

    public void Highlight()
    {
        curNum = GameManager.instance.curNumMinigame;
    }

    public void Unhighlight()
    {
        curNum = -1;
    }
}