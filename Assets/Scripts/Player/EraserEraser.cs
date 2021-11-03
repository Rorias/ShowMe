using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EraserEraser : MonoBehaviour
{
    private float liveTime = 0.5f;

    private void FixedUpdate()
    {
        liveTime -= Time.fixedDeltaTime;

        if (liveTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
