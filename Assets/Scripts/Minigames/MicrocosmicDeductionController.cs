using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MicrocosmicDeductionController : MonoBehaviour
{
    public Sprite[] images;
    public Vector2[] imageStartPositions;
    public float[] imageZoomSpeed;
    public SpriteRenderer zoomedImage;

    bool zoomout = false;
    float guessing = 0f;
    public float guessTime = 3f;
    public float zoomoutTime = 0.1f;
    public Camera cam;
    public Camera cam2;
    float startSize;

    public bool[] buzzerInputs;
    int players;

    private void Awake()
    {
        players = GameManager.instance.players;

        //Resize arrays
        System.Array.Resize(ref buzzerInputs, players);
        
        startSize = cam.orthographicSize;
        guessing = 0f;
    }

    public bool multiRound;

    private void Start()
    {
        multiRound = true;
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
            buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

            if (buzzerInputs[i])
            {
                //Play buzz sound
                GameManager.instance.PlaySound(GameManager.instance.buzzSnd);

                //Pause so they can guess what the image is
                if (guessing <= 0)
                {
                    guessing = guessTime;
                }
            }
        }
    }

    public void GetInstruction()
    {
        cam.orthographicSize = startSize;
        cam2.orthographicSize = startSize;
        zoomout = true;

        int index = Random.Range(0, images.Length - 1);
        zoomedImage.transform.position = imageStartPositions[index];
        zoomedImage.sprite = images[index];
        zoomoutTime = imageZoomSpeed[index];
    }
}
