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
    public float zoomoutTime = 0.1f;
    public Camera cam;

    //private void Awake()
    //{
    //    cam = Camera.main;
    //}

    public void StartRound()
    {
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        if (zoomout) cam.orthographicSize += zoomoutTime * Time.deltaTime;
    }

    public void GetInstruction()
    {
        zoomout = true;

        int index = Random.Range(0, images.Length);
        zoomedImage.sprite = images[index];
    }
}
