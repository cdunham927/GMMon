using UnityEngine;
using UnityEngine.InputSystem; // Don't forget this line!

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // This function is called by the "Player Input" component when the "Move" action is triggered.
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // This function is called by the "Player Input" component when the "Buzzer" action is triggered.
    public void OnButton(InputValue value)
    {
        // Check if the button was pressed down this frame
        if (value.isPressed)
        {
            Debug.Log("Button Pressed!");
            // Add your game show buzzer logic here!
            // For example, light up the player's podium or lock out other players.
        }
    }

    // Use FixedUpdate for physics-based movement
    void FixedUpdate()
    {
        // Apply the movement
        if (rb != null)
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }
}