using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public List<PanelBehaviour> panels = new List<PanelBehaviour>();

    public PanelBehaviour activePanel { get; private set; }
    private int panelIndex = 0;

    private void Awake()
    {
        activePanel = panels[panelIndex];
    }

    public void FinishPanel()
    {
        activePanel.HidePanelPlatforms();
    }

    public void SetNextPanel()
    {
        panelIndex++;

        if (panelIndex <= panels.Count - 1)
        {
            activePanel = panels[panelIndex];
        }
    }
}
