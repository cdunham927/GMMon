using UnityEngine;
using UnityEngine.Video;          // Required for VideoPlayer
using UnityEngine.EventSystems; // Required to check for UI clicks

public class MultiDisplayVideo : MonoBehaviour
{
    // Drag your VideoPlayer component here in the Inspector
    public VideoPlayer videoPlayer;

    void Start()
    {
        // This is crucial! It tells Unity to find and turn on 
        // the second monitor when the game builds.
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }

    void Update()
    {
        // Check for a left mouse click (button 0)
        if (Input.GetMouseButtonDown(0))
        {
            // This checks if your mouse is over a UI button on Display 1.
            // If it is, we don't advance the video.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Click was on a UI element, so do nothing.
            }

            // If the click was not on UI, play the video.
            if (videoPlayer != null && !videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }
}