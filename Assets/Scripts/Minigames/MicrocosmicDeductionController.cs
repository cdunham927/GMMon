using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MicrocosmicDeductionController : MonoBehaviour
{
    public Sprite[] images;
    public Image zoomedImage;

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;


    public float roundTimeSmall = 2f;
    public float roundTimeBig = 10f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    public void StartRound()
    {
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        //if (curTime > 0) curTime -= Time.deltaTime;

        //Show time remaining
        //int seconds = ((int)curTime % 60);
        //int minutes = ((int)curTime / 60);
        //timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timeRemaining2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GetInstruction()
    {
        int index = Random.Range(0, images.Length);
        zoomedImage.sprite = images[index];

        //commandText.text = currentObject;
        //yurpusCommandText.text = currentObject;

        gameObject.SetActive(false);
    }
}
