using UnityEngine;

namespace Player
{
    public class Tank_Stats : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] private float m_maxHealth = 50;
        [SerializeField] private float m_currentHealth;

        [Header("Movement Settings")]
        [SerializeField] protected float m_tankMovementSpeed = 12.5f;
        [SerializeField] protected float m_tankRotationSpeed = 100f;

        [Header("Fire Settings")]
        [SerializeField] private GameObject m_projectile;
        [SerializeField] protected float m_launchVelocity = 500f;
        [SerializeField] protected float m_reloadTime = 1.0f;

        void Start()
        {
            m_currentHealth = m_maxHealth;
        }

        private void TakeDamage(int damage)
        {
            if ((m_currentHealth - damage) <= 0)
            {
                IsDead();
            }

            m_currentHealth -= damage;
        }

        private bool IsAlive()
        {
            return m_currentHealth > 0;
        }

        private void IsDead()
        {
            m_currentHealth = 0;
        }

        public GameObject GetProjectile()
        {
            return m_projectile;
        }
    }

}

