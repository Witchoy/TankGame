using UnityEngine;

namespace Projectiles
{
    // Ensures the GameObject has a VFX
    [RequireComponent(typeof(GameObject))]
    public class ShellController : MonoBehaviour
    {
        [Header("VFX Component")]
        [SerializeField] private GameObject m_vfxExplosion;

        void OnCollisionEnter(Collision collision)
        {
            Instantiate(m_vfxExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}