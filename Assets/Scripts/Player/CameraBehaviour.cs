using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private PanelManager panelManager;

    public Transform target;

    public float rightBound;
    public float leftBound;
    public float topBound;
    public float bottomBound;

    private float zoomDelayTimer = 0.0f;
    public float maxZoomDelayTime;

    private bool followPlayer = true;
    private bool zooming = true;

    private void Awake()
    {
        panelManager = GameObject.Find("GameManager").GetComponent<PanelManager>();
    }

    private void Update()
    {
        if (followPlayer && !zooming)
        {
            Vector3 pos = new Vector3(target.position.x, target.position.y + 1f, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
            pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
            transform.position = pos;
        }
    }

    private void FixedUpdate()
    {
        zoomDelayTimer += Time.fixedDeltaTime;

        if (zoomDelayTimer >= maxZoomDelayTime)
        {
            if (!followPlayer)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 9, 0.05f / (9 - Camera.main.orthographicSize));
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(target.position.x, target.position.y, -10), 0.1f / Vector2.Distance(Camera.main.transform.position, new Vector3(target.position.x, target.position.y, -10)));

                if (Camera.main.orthographicSize >= 8.9f)
                {
                    zooming = false;
                    Camera.main.orthographicSize = 9;

                }
            }
            else
            {
                Vector3 pos = new Vector3(target.position.x, target.position.y + 1f, transform.position.z);
                pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
                pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);

                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4.5f, 0.05f / (Camera.main.orthographicSize - 4.5f));
                Camera.main.transform.position = Vector3.Lerp(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10), new Vector3(pos.x, pos.y, -10), 0.1f / Vector2.Distance(Camera.main.transform.position, new Vector3(pos.x, pos.y, -10)));

                if (Camera.main.orthographicSize <= 4.6f)
                {
                    zooming = false;
                    Camera.main.orthographicSize = 4.5f;
                }
            }
        }
    }

    public void ZoomToPanelCenter()
    {
        zoomDelayTimer = 0.0f;
        followPlayer = false;
        zooming = true;
        target = panelManager.activePanel.transform;
        panelManager.FinishPanel();
    }
}
