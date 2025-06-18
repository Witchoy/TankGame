using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    // Ensures the GameObject has a VFX
    [RequireComponent(typeof(Collider))]
    public class ShellController : MonoBehaviour
    {
        [Header("VFX Component")]
        [SerializeField] private GameObject m_vfxExplosion;

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("j'ai hit : " + collision.GetType());
            if (collision.gameObject.CompareTag("DestructibleWall"))
            {
                Debug.Log("Il avait le tag mur destructibles !");
                collision.gameObject.GetComponent<Destructible_Wall>().SetToBroken();
            }
            // Calculate the rotation opposed to the shell's facing direction
            Quaternion opposedRotation = Quaternion.LookRotation(-transform.forward, transform.up);
            Instantiate(m_vfxExplosion, transform.position, opposedRotation);
            Destroy(gameObject);
        }
    }
}