using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Tank_Inputs : MonoBehaviour
    {
        public InputActionReference m_moveAction;
        public InputActionReference m_rotateAction;
        public InputActionReference m_fireAction;

        private float m_forwardInput;   // Forward/backward movement input
        private float m_rotationInput;  // Left/right rotation input
        private bool m_fireInput; // Fire input

        void Update()
        {
            HandleInputs();
            HandlePrintInput();
        }
        
        private void HandlePrintInput()
        {
            if (Keyboard.current != null)
            {
                foreach (var keyControl in Keyboard.current.allKeys)
                {
                    if (keyControl.wasPressedThisFrame)
                    {
                        Debug.Log($"Key pressed: {keyControl.displayName}");
                    }
                }
            }
        }

        // Handles player input each frame
        protected void HandleInputs()
        {
            // Get player input from keyboard
            m_forwardInput = m_moveAction.action.ReadValue<float>();
            m_rotationInput = m_rotateAction.action.ReadValue<float>();
            m_fireInput = m_fireAction.action.IsPressed();
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
