using UnityEngine;

public class DashGhostBehaviour : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 0.3f);
    }
}
