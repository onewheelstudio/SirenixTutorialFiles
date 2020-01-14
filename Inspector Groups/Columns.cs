using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Columns : MonoBehaviour
{
    [HorizontalGroup("Base", LabelWidth = 80)]

    [VerticalGroup("Base/Column 1")]
    public string a;


    [VerticalGroup("Base/Column 1")]
    public string b;

    [BoxGroup("Base/Column 2")]
    public string c;
    [BoxGroup("Base/Column 2")]
    public string d;
}
