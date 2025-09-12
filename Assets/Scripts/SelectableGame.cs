using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableGame : MonoBehaviour, IPointerClickHandler
{
    public string gameName;
    Button b;

    void Awake()
    {
        b = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AddGame();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SubGame();
        }
    }

    void AddGame()
    {
        GameManager.instance.AddMinigame(gameName);
    }

    void SubGame()
    {
        GameManager.instance.SubMinigame(gameName);
    }
}
