using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    public InputActionReference swipe;

    public InputActionAsset inputActions; // Reference to the input actions file
    private InputAction touchPositionAction; // Action to track touch position
    private Vector2 startPos, endPos; // Variables to store start and end touch position
    private bool isSwiping; // Whether swipe has started or not


    void OnEnable()
    {
        // Get the action map named "Player" and find the "Touch" action inside it
        var gameplayMap = inputActions.FindActionMap("Player");
        touchPositionAction = gameplayMap.FindAction("Touch");
        touchPositionAction.Enable();

        // Attach functions to touch events
        if (Touchscreen.current == null)
            return;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.press.wasPressedThisFrame)
            {
                StartSwipe();
            }
            else if (touch.press.wasReleasedThisFrame)
            {
                EndSwipe();
            }
        }
    }
    void StartSwipe()
    {
        // Store the position where the finger touches the screen
        startPos = touchPositionAction.ReadValue<Vector2>();
        isSwiping = true;
    }
    void EndSwipe()
    {
        // Only run if swipe actually started
        if (!isSwiping) return;
        // Get the position where the finger lifted off the screen
        endPos = touchPositionAction.ReadValue<Vector2>();
        // Calculate the swipe vector
        Vector2 swipe = endPos - startPos;
        // Call a function to detect direction
        DetectSwipe(swipe);
        // Reset swipe flag
        isSwiping = false;
    }
    void DetectSwipe(Vector2 swipe)
    {
        // If the swipe is too small, ignore it
        if (swipe.magnitude < 50) return; // You can adjust this threshold

        // Check if horizontal swipe is stronger
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            if (swipe.x > 0)
                Debug.Log("Swipe Right");
            else
                Debug.Log("Swipe Left");
        }
        else
        {
            if (swipe.y > 0)
                Debug.Log("Swipe Up");
            else
                Debug.Log("Swipe Down");
        }
    }
}
