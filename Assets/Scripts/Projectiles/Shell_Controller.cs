using UnityEngine;

namespace Projectiles
{
    // Ensures the GameObject has a VFX
    [RequireComponent(typeof(Collider))]
    public class ShellController : MonoBehaviour
    {
        [Header("VFX Component")]
        [SerializeField] private GameObject m_vfxExplosion;

        void OnCollisionEnter(Collision collision)
        {
            // Calculate the rotation opposed to the shell's facing direction
            Quaternion opposedRotation = Quaternion.LookRotation(collision.transform.forward, collision.transform.up);
            Instantiate(m_vfxExplosion, transform.position, opposedRotation);
            Destroy(gameObject);
        }
    }
}