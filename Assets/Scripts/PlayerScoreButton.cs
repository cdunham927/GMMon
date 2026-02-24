using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerScoreButton : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public int playerPlace;
    IPlayerOut pOut;
    public bool outIn;
    public bool clickToLose = false;
    public int thisPlayer;
    [HideInInspector]
    public TMP_Text text;
    Image i;
    public int num;

    Transform buttonParent;
    bool reparented = false;

    public Sprite[] regSprites;
    public Sprite[] inSprites;
    public Sprite[] outSprites;

    public Material regMat;
    public Material highlightMat;

    public ParticleSystem parts;
    public int emitAmount;

    Animator anim;
    [HideInInspector]
    public PlayerScore monitor2Score;

    public Image numberImage;
    public Sprite[] numbers;
    public Sprite emptyImage;

    public int maxScore = 99;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerPlace = -1;
        text = GetComponentInChildren<TMP_Text>();
        pOut = FindObjectOfType<IPlayerOut>();
        i = GetComponent<Button>().GetComponent<Image>();
        i.material = regMat;

        buttonParent = transform.parent;
    }

    public void Confetti()
    {
        if (parts != null) parts.Emit(emitAmount);
    }

    public void Winner()
    {
        i.material = highlightMat;
        anim.Play("PlayerScoreWin");
        monitor2Score.Winner();
        Confetti();
    }

    private void Update()
    {
        //if (!outIn) text.text = "Player " + (thisPlayer + 1).ToString() + " - " + GameManager.instance.playerScores[thisPlayer].ToString();

        if (!outIn)
        {
            i.sprite = regSprites[num];
        }

        //Just the score, if the image shows what each player number is
        if (!outIn) {
            text.text = GameManager.instance.playerScores[thisPlayer].ToString();
            if (numberImage != null) numberImage.sprite = numbers[GameManager.instance.playerScores[thisPlayer]];
        }
        else {
            text.text = "";
            if (numberImage != null) numberImage.sprite = emptyImage;
        }

        //Write what place each 'out' player is in
        //if (outIn)
        //{
        //    string t = "";
        //    if (playerPlace != -1)
        //    {
        //        if (playerPlace == 1) t = (playerPlace).ToString() + "st";
        //        else if (playerPlace == 2) t = (playerPlace).ToString() + "nd";
        //        else if (playerPlace == 3) t = (playerPlace).ToString() + "rd";
        //        else t = (playerPlace).ToString() + "th";
        //
        //        text.text = "Player " + (thisPlayer + 1).ToString() + " - " + t;
        //    }
        //    else {
        //        if (pOut.curPlace == 1) t = (pOut.curPlace) + "st";
        //        else if (pOut.curPlace == 2) t = (pOut.curPlace) + "nd";
        //        else if (pOut.curPlace == 3) t = (pOut.curPlace) + "rd";
        //        else t = (pOut.curPlace).ToString() + "th";
        //
        //        text.text = "Player " + (thisPlayer + 1).ToString() + " - " + t;
        //    }
        if (outIn) 
        {
            if (regSprites.Length >= num)
            {
                i.sprite = (playerPlace == -1) ? inSprites[num] : outSprites[num];
            }
            else
            {
                i.color = (playerPlace == -1) ? Color.white : Color.red;
            }
        }
    }

    public void AddScore()
    {
        if (GameManager.instance.playerScores[thisPlayer] < maxScore)
        {
            GameManager.instance.playerScores[thisPlayer]++;
            anim.Play("PlayerScoreGet");
            monitor2Score.GetPoint();
        }
    }

    public void SubScore()
    {
        if (GameManager.instance.playerScores[thisPlayer] > 0)
        {
            GameManager.instance.playerScores[thisPlayer]--;
            anim.Play("PlayerScoreLose");
            monitor2Score.LosePoint();
        }
    }

    public void PlayerOut()
    {
        if (playerPlace == -1)
        {
            anim.Play("PlayerScoreLose");
            monitor2Score.LosePoint();
            //Change color to red
            playerPlace = pOut.curPlace;

            pOut.eliminatedPlayer++;
            pOut.curPlace--;

            if (!reparented)
            {
                reparented = true;
                transform.SetParent(null);
                transform.SetParent(buttonParent);
            }
        }
    }

    public void PlayerIn()
    {
        if (playerPlace != -1)
        {
            anim.Play("PlayerScoreGet");
            monitor2Score.GetPoint();
            //Change color to white
            playerPlace = -1;
            pOut.curPlace++;

            pOut.eliminatedPlayer--;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (outIn == false && clickToLose == false)
            {
                GameManager.instance.PlayRandomSound(GameManager.instance.playerAddScoreSounds);
                AddScore();
            }
            else
            {
                GameManager.instance.PlayRandomSound(GameManager.instance.playerSubScoreSounds);
                PlayerOut();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Right Clicked");
            if (outIn == false && clickToLose == false)
            {
                GameManager.instance.PlayRandomSound(GameManager.instance.playerSubScoreSounds);
                SubScore();
            }
            else
            {
                GameManager.instance.PlayRandomSound(GameManager.instance.playerAddScoreSounds);
                PlayerIn();
            }
        }
    }
}
