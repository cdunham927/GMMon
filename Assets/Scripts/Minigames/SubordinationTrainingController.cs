using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubordinationTrainingController : MonoBehaviour
{
    public string[] principles = { 
        "When the timer's auditory signal has concluded, you are to perform the absolute logical antithesis of the following directive: " +
        "it is imperative that you avoid failing to abstain from initiating physical contact with Yurpus.",
        
        "Be it enacted and understood that the Operator, hereafter referred to as 'the Participant,' is contractually obligated to " +
        "perform a series of actions within the prescribed temporal period, said period commencing immediately and expiring upon " +
        "the issuance of the terminal auditory notification by the timing mechanism. The Participant's primary obligation during this period " +
        "is to engage with the signal buzzer apparatus. This engagement shall consist of a sequence of distinct, non-simultaneous manual " +
        "depressions, the total sum of which must be no less than and no greater than the number of appendages on a standard human hand. " +
        "Failure to complete the full quintuple sequence prior to the temporal expiration event will constitute a material breach of protocol.",

        "Say Yurpus." };

    public string[] hints = { "Are you smarter than a biggusglorbo?", "The fractal trees on Yagrina 5 are purple.", "You are dumb, but perfect for being pets." };

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
    float curTime;
    public TMP_Text timeRemaining;

    public void StartRound()
    {
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        if (curTime > 0) curTime -= Time.deltaTime;

        //Show time remaining
        int seconds = ((int)curTime % 60);
        int minutes = ((int)curTime / 60);
        timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GetInstruction()
    {
        curTime = minigameTime;

        int index = Random.Range(0, principles.Length - 1);

        commandText.text = principles[index];
        yurpusCommandText.text = "Hint: " + hints[index];

        //gameObject.SetActive(false);
    }
}
