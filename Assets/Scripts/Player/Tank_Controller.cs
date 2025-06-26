using System.Collections;
using UnityEngine;

namespace Player
{
    // Ensures the GameObject has a Rigidbody and Tank_Inputs and Tank_Stats component
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tank_Inputs))]
    [RequireComponent(typeof(Tank_Stats))]
    public class Tank_Controller : MonoBehaviour
    {
        private Rigidbody m_rigidbody;
        private Tank_Inputs m_inputs;

        [Header("Movement Settings")]
        [SerializeField] protected float m_tankMovementSpeed = 100000f;
        [SerializeField] protected float m_tankRotationSpeed = 100f;

        [Header("Fire Settings")]
        [SerializeField] private GameObject m_projectile;
        [SerializeField] protected float m_launchVelocity = 500f;
        [SerializeField] protected float m_reloadTime = 1.0f;

        private bool m_isReloading = false;

        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_inputs = GetComponent<Tank_Inputs>();

            // Warn if no projectile prefab is assigned
            if (m_projectile == null)
            {
                Debug.LogWarning("Projectile prefab not assigned!");
                return;
            }
        }

        // Called once per frame (used for inputs like firing)
        void Update()
        {
            if (m_inputs.GetFireInput() && !m_isReloading)
            {
                HandleFireShell();
                StartCoroutine(Reload());
            }
        }

        // Called at a fixed interval (used for physics-based movement)
        void FixedUpdate()
        {
            HandleMovementWithPosition();
        }

        protected void HandleMovementWithPosition()
        {
            float delta = Time.fixedDeltaTime;

            Vector3 newPos = transform.position + (transform.forward * m_inputs.GetForwardInput() * m_tankMovementSpeed * delta);
            m_rigidbody.MovePosition(newPos);

            Quaternion newRot = transform.rotation * Quaternion.Euler(Vector3.up * (m_tankRotationSpeed * m_inputs.GetRotationInput() * delta));
            m_rigidbody.MoveRotation(newRot);
        }

        // Handles tank movement and rotation
        protected void HandleMovementWithForce()
        {
            // Forward/backward movement
            float forwardInput = m_inputs.GetForwardInput();
            if (Mathf.Abs(forwardInput) > 0.1f)
            {
                Vector3 force = transform.forward * forwardInput * m_tankMovementSpeed;
                m_rigidbody.AddForce(force, ForceMode.Force);
            }

            // Tank rotation with torque
            float rotationInput = m_inputs.GetRotationInput();
            if (Mathf.Abs(rotationInput) > 0.1f)
            {
                float torqueAmount = rotationInput * m_tankRotationSpeed;
                Vector3 torque = Vector3.up * torqueAmount;
                m_rigidbody.AddTorque(torque, ForceMode.Force);
            }
        }


        // Handles firing the projectile
        protected void HandleFireShell()
        {
            // Offset the spawn position slightly in front and at the tank's cannon height
            Vector3 spawnOffset = transform.forward * 4f + Vector3.up * 3f;
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Adjust rotation so the projectile faces forward correctly
            Quaternion spownRotation = Quaternion.Euler(90, transform.eulerAngles.y, transform.eulerAngles.z);

            // Spawn the projectile at the given position and rotation
            GameObject projectile = Instantiate(m_projectile, spawnPosition, spownRotation);

            // Apply velocity to the projectile in the tank's forward direction
            projectile.GetComponent<Rigidbody>().linearVelocity = transform.forward * m_launchVelocity;
        }

        IEnumerator Reload()
        {
            m_isReloading = true;

            yield return new WaitForSeconds(m_reloadTime);

            m_isReloading = false;
        }

        // Check if the tank wheels are on the ground
        private bool IsGrounded()
        {
            float checkDistance = 1f;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitinfo, checkDistance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
