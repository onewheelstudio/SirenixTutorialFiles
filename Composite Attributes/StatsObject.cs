using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StatsObject : MonoBehaviour
{
    [PropertyOnly]
    public Stats stats;

    [PreFabAssetList]
    public GameObject prefabList;
}
