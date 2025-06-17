using UnityEngine;

namespace Player
{
    public class Tank_Inputs : MonoBehaviour
    {
        private float m_forwardInput;   // Forward/backward movement input
        private float m_rotationInput;  // Left/right rotation input
        private bool m_fireInput;

        void Update()
        {
            HandleInputs();
        }

        // Handles player input each frame
        protected void HandleInputs()
        {
            // Get player input from keyboard
            m_forwardInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys
            m_rotationInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
            m_fireInput = Input.GetMouseButtonDown(0); // Left Click
        }

        // Returns the forward movement input
        public float GetForwardInput()
        {
            return m_forwardInput;
        }

        // Returns the rotation input
        public float GetRotationInput()
        {
            return m_rotationInput;
        }

        // Returns the fire input
        public bool GetFireInput()
        {
            return m_fireInput;
        }
    }
}
