using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Player.Tank_Inputs))]
    public class Tank_Controller : MonoBehaviour
    {
        private Rigidbody m_rigidbody;
        private Tank_Inputs m_inputs;

        [Header("Movement Settings")]
        public float m_tankMovementSpeed = 12.5f;
        public float m_tankRotationSpeed = 100f;

        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_inputs = GetComponent<Tank_Inputs>();
        }

        void FixedUpdate()
        {
            if (m_rigidbody && m_inputs)
            {
                HandleMovement();
            }
        }

        protected void HandleMovement()
        {
            float delta = Time.fixedDeltaTime;

            Vector3 movement = m_rigidbody.position + transform.forward * m_inputs.getForwardInput() * m_tankMovementSpeed * delta;
            m_rigidbody.MovePosition(movement);

            float rotationAmount = m_tankRotationSpeed * m_inputs.getRotationInput() * delta;
            Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * rotation);
        }
    }
}
