using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InstructionsController : MonoBehaviour
{
    VideoPlayer video;
    public VideoClip titleVideo;
    public VideoClip instructionVideo;
    public GameObject[] instructions;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.clip = titleVideo;
        video.Play();
        instructions = GameObject.FindGameObjectsWithTag("Instructions");
        Invoke("SwitchVideo", (float)titleVideo.length);
    }

    void SwitchVideo()
    {
        video.clip = instructionVideo;
        video.Play();
    }

    public void DeactivateVideo()
    {
        video.gameObject.SetActive(false);

        foreach (GameObject g in instructions)
        {
            g.SetActive(false);
        }
    }
}
