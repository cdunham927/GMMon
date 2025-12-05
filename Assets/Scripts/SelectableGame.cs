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

    void Awake()
    {
        b = GetComponent<Button>();
        originalColor = b.image.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AddGame();
            Highlight();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SubGame();
            Unhighlight();
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

    public void Highlight()
    {
        b.image.color = Color.yellow;
    }

    public void Unhighlight()
    {
        b.image.color = originalColor;
    }
}