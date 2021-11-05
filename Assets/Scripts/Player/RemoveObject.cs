using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    public float liveTime;

    private void Awake()
    {
        Destroy(gameObject, liveTime);
    }
}
