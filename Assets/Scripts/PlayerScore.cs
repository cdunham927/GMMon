using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    MenuFunctions menu;
    public int num;
    public PlayerScoreButton parent;
    TMP_Text text;
    Image i;
    public Image catImpImage;

    public Sprite[] regSprites;
    public Sprite[] inSprites;
    public Sprite[] outSprites;

    Transform buttonParent;
    bool reparented = false;

    public Material regMat;
    public Material highlightMat;

    //public ParticleSystem parts;
    //public int emitAmount;

    private void Awake()
    {
        menu = FindObjectOfType<MenuFunctions>();
        text = GetComponentInChildren<TMP_Text>();
        i = GetComponentInChildren<Image>();
        i.material = regMat;

        parent = menu.playerScores[num];
        buttonParent = transform.parent;
    }

    //public void Confetti()
    //{
    //    parts.Emit(emitAmount);
    //}

    public void Winner()
    {
        i.material = highlightMat;
    }


    private void Update()
    {
        if (parent != null) text.text = parent.text.text;

        //Reparent
        if (parent.playerPlace != -1 && reparented == false)
        {
            reparented = true;
            transform.SetParent(null);
            transform.SetParent(buttonParent);
        }

        if (!parent.outIn)
        {
            i.sprite = regSprites[num];
        }

        if (parent.outIn)
        {
            if (regSprites.Length >= num)
            {
                i.sprite = (parent.playerPlace == -1) ? inSprites[num] : outSprites[num];
            }
            else
            {
                i.color = (parent.playerPlace == -1) ? Color.white : Color.red;
            }
        }
    }
}
