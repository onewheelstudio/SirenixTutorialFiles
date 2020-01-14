using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Tabs : MonoBehaviour
{
    [TabGroup("Tab Group 1", "Tab 1")]
    public string A;
    [TabGroup("Tab Group 1", "Tab 1")]
    public string B;

    [TabGroup("Tab Group 1", "Tab 2")]
    public int C;
    [TabGroup("Tab Group 1", "Tab 2")]
    public int D;

    [BoxGroup("Box Group")]
    public int E;

    [BoxGroup("Tab Group 1/Tab 1/Sub Box Group")]
    public int F;
}
