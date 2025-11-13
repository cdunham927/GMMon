using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MicrocosmicDeductionController : MonoBehaviour
{
    public Sprite[] images;
    public SpriteRenderer zoomedImage;

    bool zoomout = false;
    float guessing = 0f;
    public float guessTime = 3f;
    public float zoomoutTime = 0.1f;
    public Camera cam;
    float startSize;

    private void Start()
    {
        startSize = cam.orthographicSize;
        guessing = 0f;
    }

    public void StartRound()
    {
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        if (zoomout && guessing <= 0f) cam.orthographicSize += zoomoutTime * Time.deltaTime;

        if (guessing > 0) guessing -= Time.deltaTime;


    }

    public void GetInstruction()
    {
        cam.orthographicSize = startSize;
        zoomout = true;

        int index = Random.Range(0, images.Length);
        zoomedImage.sprite = images[index];
    }
}
