using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vector2SliderAttribute : Attribute
{
    public float minValue;
    public float maxValue;

    public Vector2SliderAttribute(float min, float max)
    {
        this.minValue = min;
        this.maxValue = max;
    }
}
