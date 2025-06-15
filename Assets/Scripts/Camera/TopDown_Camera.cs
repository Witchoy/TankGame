using UnityEngine;

namespace Cameras
{
    public class TopDown_Camera : MonoBehaviour
    {
        [Header("Target & View Settings")]
        public Transform m_target;
        public float m_height = 10f;
        public float m_distance = 20f;
        public float m_angle = 45f;
        public float m_smoothSpeed = 0.5f;

        private Vector3 refVelocity;

        // Called once at the start
        void Start()
        {
            HandleCamera();
        }

        // Called once per frame
        void Update()
        {
            HandleCamera();
        }

        // Calculates and applies the camera's position and rotation relative to the target.
        protected void HandleCamera()
        {
            // Make sure a target has been assigned
            if (!m_target)
            {
                Debug.Log("No target assigned !");
                return;
            }

            // Create a base offset vector: backwards along Z and upwards along Y
            Vector3 worldPosition = (Vector3.forward * -m_distance) + (Vector3.up * m_height);

            // Rotate the offset vector around the Y-axis by m_angle degrees
            Vector3 rotatedVector = Quaternion.AngleAxis(m_angle, Vector3.up) * worldPosition;

            // Flatten the target's position to avoid undesired Y influence (optional)
            Vector3 flatTargetPosition = m_target.position;
            flatTargetPosition.y = 0f;

            // Final camera position is the rotated offset added to the flattened target position
            Vector3 finalPosition = flatTargetPosition + rotatedVector;

            // Smoothly move the camera to the final position
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_smoothSpeed);

            // Make the camera look at the target's actual position
            transform.LookAt(m_target.position);
        }
    }
}
