using UnityEngine;

// Ensure the GameObject has a Rigidbody and Tank_Inputs component
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Tank_Inputs))]
public class Tank_Controller : MonoBehaviour
{
    private Rigidbody m_rigidbody;     // Reference to the Rigidbody component
    private Tank_Inputs m_inputs;      // Reference to the input handler

    [Header("Movement Settings")]
    public float m_tankMovementSpeed = 12.5f;            // Tank movement speed
    public float m_tankRotationSpeed = 100f;  // Tank rotation speed

    // Called before the first frame update
    void Start()
    {
        // Get the required components
        m_rigidbody = GetComponent<Rigidbody>();
        m_inputs = GetComponent<Tank_Inputs>();
    }

    // Called at fixed intervals (best for physics calculations)
    void FixedUpdate()
    {
        // Ensure both components are available
        if (m_rigidbody && m_inputs)
        {
            HandleMovement();
        }
    }

    // Moves and rotates the tank based on player input
    protected void HandleMovement()
    {
        float delta = Time.fixedDeltaTime;

        // Move the tank forward or backward
        Vector3 movement = m_rigidbody.position + transform.forward * m_inputs.getForwardInput() * m_tankMovementSpeed * delta;
        m_rigidbody.MovePosition(movement);

        // Rotate the tank left or right
        float rotationAmount = m_tankRotationSpeed * m_inputs.getRotationInput() * delta;
        Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
        m_rigidbody.MoveRotation(m_rigidbody.rotation * rotation);
    }
}
