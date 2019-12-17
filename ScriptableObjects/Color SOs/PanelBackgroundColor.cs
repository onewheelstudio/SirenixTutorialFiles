using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PanelBackgroundColor : MonoBehaviour
{
    private Image panel;
    public ColorData color;

    private void AdjustColor(ColorReference _color)
    {
        if (panel == null)
            panel = this.GetComponent<Image>();

        panel.color = _color.Value;
    }
}
