using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Grouping : MonoBehaviour
{
    [HorizontalGroup("base", Width = 150)]

    [VerticalGroup("base/left")]
    [LabelWidth(90)]
    [HideLabel, Title("Enemy Name", Bold = false, HorizontalLine = false)]
    public string enemyName;
    [VerticalGroup("base/left")]
    [PreviewField(150)]
    [HideLabel]
    public Texture2D texture;

    [VerticalGroup("base/right")]
    [TextArea(5,5)]
    public string description;

    [HorizontalGroup("base/right/lower")]

    [VerticalGroup("base/right/lower/left")]


    [LabelWidth(50)]
    [Range(0,20)]
    public float stat1;

    [VerticalGroup("base/right/lower/left")]

    [LabelWidth(50)]
    [Range(0,20)]
    public float stat2;

    [VerticalGroup("base/right/lower/right")]

    [LabelWidth(50)]
    [Range(0,20)]
    public float stat3;
    [VerticalGroup("base/right/lower/right")]
    [LabelWidth(50)]
    [Range(0,20)]
    public float stat4;

}
