using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class Vector2SliderAttributeDrawer : OdinAttributeDrawer<Vector2SliderAttribute, Vector2>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        Rect rect = EditorGUILayout.GetControlRect();

        if(label != null)
            rect = EditorGUI.PrefixLabel(rect, label);

        Vector2 value = this.ValueEntry.SmartValue;

        GUIHelper.PushLabelWidth(20);
        value.x = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.5f), "X", value.x,
            this.Attribute.minValue, this.Attribute.maxValue);
        value.y = EditorGUI.Slider(rect.AlignRight(rect.width * 0.5f), "Y", value.y,
            this.Attribute.minValue, this.Attribute.maxValue);
        GUIHelper.PopLabelWidth();

        this.ValueEntry.SmartValue = value;
    }
}
