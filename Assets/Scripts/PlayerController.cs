using UnityEngine;
using UnityEngine.InputSystem; // Make sure this line is here!

public class PlayerController : MonoBehaviour
{
    // The name "OnAnswer" and the parameter (InputAction.CallbackContext context)
    // MUST be exactly like this.
    public void OnAnswer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int playerNumber = GetComponent<PlayerInput>().playerIndex + 1;
            Debug.Log($"Player {playerNumber} buzzed in!");
        }
    }

    // The name "OnMove" and the parameter (InputAction.CallbackContext context)
    // MUST also be exactly like this.
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.magnitude > 0.1f)
        {
            int playerNumber = GetComponent<PlayerInput>().playerIndex + 1;
            Debug.Log($"Player {playerNumber} is moving: {moveInput}");
        }
    }
}