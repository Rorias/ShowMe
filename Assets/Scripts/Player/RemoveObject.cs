using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    public float liveTime;

    private void FixedUpdate()
    {
        liveTime -= Time.fixedDeltaTime;

        if (liveTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
