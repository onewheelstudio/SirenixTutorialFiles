using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class TestClass : MonoBehaviour
{
    [NeedsComponent(typeof(Rigidbody))]
    public GameObject componentTest;

    [SpriteSize(100)]
    public Sprite sprite;

    [SpriteSize(SpriteSize.large)]
    public Sprite sprite2;

    //public List<Plant> plantList = new List<Plant>();

}

