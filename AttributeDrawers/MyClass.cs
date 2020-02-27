using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    [Vector2Slider(0,20)]
    public Vector2 myVector2;
    
    [MyColor]
    public Color myColor;

}

