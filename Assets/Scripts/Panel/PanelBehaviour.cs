using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
    public List<GameObject> panelPlatformsToHide = new List<GameObject>();
    public GameObject panelDoor;

    private void Start()
    {

    }

    public void HidePanelPlatforms()
    {
        for (int i = 0; i < panelPlatformsToHide.Count; i++)
        {
            panelPlatformsToHide[i].SetActive(false);
        }
    }

    public void Open()
    {
        panelDoor.GetComponent<Animator>().SetBool("Vanish", true);
    }

    public void Close()
    {
        SpriteRenderer sr = panelDoor.GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255);

        panelDoor.GetComponent<BoxCollider2D>().enabled = true;
    }
}
