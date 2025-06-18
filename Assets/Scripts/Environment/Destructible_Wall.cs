using UnityEngine;

public class Destructible_Wall : MonoBehaviour
{
    [Header("Wall Object")]
    [SerializeField] GameObject m_brokenWall;

    public void SetToBroken()
    {
        Debug.Log("SetToBroken() Called !");
        gameObject.SetActive(false);
        m_brokenWall.SetActive(true);
    }
}
