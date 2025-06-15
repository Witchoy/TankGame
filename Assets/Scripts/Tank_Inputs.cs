using UnityEngine;

public class Tank_Inputs : MonoBehaviour
{
    [Header("Input Properties")]
    public Camera m_camera; // Reference to the camera used for input

    private float m_forwardInput;   // Forward/backward movement input
    private float m_rotationInput;  // Left/right rotation input

    void Update()
    {
        // Only handle inputs if the camera is assigned
        if (m_camera)
        {
            HandleInputs();
        }
    }

    // Handles player input each frame
    protected void HandleInputs()
    {
        // Get player input from keyboard
        m_forwardInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys
        m_rotationInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
    }

    // Returns the forward movement input
    public float getForwardInput()
    {
        return m_forwardInput;
    }

    // Returns the rotation input
    public float getRotationInput()
    {
        return m_rotationInput;
    }
}
