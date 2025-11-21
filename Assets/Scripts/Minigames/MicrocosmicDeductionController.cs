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
    public Camera cam2;
    float startSize;

    public bool[] joystickInputs;
    int players;

    private void Awake()
    {
        players = GameManager.instance.players;

        //Resize arrays
        System.Array.Resize(ref joystickInputs, players);
        
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
        if (zoomout && guessing <= 0f)
        {
            cam.orthographicSize += zoomoutTime * Time.deltaTime;
            cam2.orthographicSize += zoomoutTime * Time.deltaTime;
        }

        if (guessing > 0) guessing -= Time.deltaTime;

        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            joystickInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

            if (joystickInputs[i] && guessing <= 0)
            {
                guessing = guessTime;
            }
        }
    }

    public void GetInstruction()
    {
        cam.orthographicSize = startSize;
        cam2.orthographicSize = startSize;
        zoomout = true;

        int index = Random.Range(0, images.Length);
        zoomedImage.sprite = images[index];
    }
}
