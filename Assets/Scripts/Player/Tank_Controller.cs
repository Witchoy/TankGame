using System.Collections;
using UnityEngine;

namespace Player
{
    // Ensures the GameObject has a Rigidbody and Tank_Inputs component
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tank_Inputs))]
    public class Tank_Controller : MonoBehaviour
    {
        private Rigidbody m_rigidbody;
        private Tank_Inputs m_inputs;

        [Header("Movement Settings")]
        [SerializeField] private float m_tankMovementSpeed = 12.5f;
        [SerializeField] private float m_tankRotationSpeed = 100f;

        [Header("Fire Settings")]
        [SerializeField] private GameObject m_projectile;
        [SerializeField] private float m_launchVelocity = 500f;
        [SerializeField] private float m_reloadTime = 1.0f;

        private bool m_isReloading = false;
        private bool m_canShoot = true;

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
            HandleMovement();
        }

        // Handles tank movement and rotation
        protected void HandleMovement()
        {
            float delta = Time.fixedDeltaTime;

            // Move the tank forward/backward based on input
            Vector3 movement = m_rigidbody.position + transform.forward * m_inputs.GetForwardInput() * m_tankMovementSpeed * delta;
            m_rigidbody.MovePosition(movement);

            // Rotate the tank based on input
            float rotationAmount = m_tankRotationSpeed * m_inputs.GetRotationInput() * delta;
            Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * rotation);
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
            m_canShoot = false;

            yield return new WaitForSeconds(m_reloadTime);

            m_canShoot = true;
            m_isReloading = false;
        }
    }
}
