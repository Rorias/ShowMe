using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float rightBound;
    public float leftBound;
    public float topBound;
    public float bottomBound;

    public Transform target;

    [HideInInspector] public bool followPlayer = true;

    private void Update()
    {
        if (followPlayer)
        {
            Vector3 pos = new Vector3(target.position.x, target.position.y + 1f, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
            pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
            transform.position = pos;
        }
    }

    private void FixedUpdate()
    {
        if(!followPlayer)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 9, 0.01f);
            Camera.main.transform.position = Vector3.Lerp(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10), new Vector3(0, 0, -10), 0.1f / Vector2.Distance(Camera.main.transform.position, new Vector3(0, 0, -10)));
        }
    }

    public void ZoomToCenter()
    {
        followPlayer = false;
    }
}
