using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class MyColorAttributeDrawer : OdinAttributeDrawer<MyColorAttribute, Color>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        Rect rect = EditorGUILayout.GetControlRect();
        Color tempColor = this.ValueEntry.SmartValue;
        string hexCode = ColorUtility.ToHtmlStringRGB(tempColor);

        if (label != null)
            rect = EditorGUI.PrefixLabel(rect, label);

        rect = EditorGUILayout.GetControlRect();
        tempColor = SirenixEditorFields.ColorField(rect.AlignLeft(rect.width * 0.75f), tempColor);

        //hexcode
        hexCode = SirenixEditorFields.TextField(rect.AlignRight(rect.width * 0.25f), "#" + hexCode);
        if(ColorUtility.TryParseHtmlString(hexCode, out tempColor))
        {
            this.ValueEntry.SmartValue = tempColor;
        }

        //rgb values
        rect = EditorGUILayout.GetControlRect();

        GUIHelper.PushLabelWidth(15);
        tempColor.r = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.3f), "R", tempColor.r, 0f, 1f);
        tempColor.g = EditorGUI.Slider(rect.AlignCenter(rect.width * 0.3f), "G", tempColor.g, 0f, 1f);
        tempColor.b = EditorGUI.Slider(rect.AlignRight(rect.width * 0.3f), "B", tempColor.b, 0f, 1f);
        GUIHelper.PopLabelWidth();

        //hsv values
        Color.RGBToHSV(tempColor, out float h, out float s, out float v);
        rect = EditorGUILayout.GetControlRect();

        GUIHelper.PushLabelWidth(15);
        h = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.3f), "H", h, 0f, 1f);
        s = EditorGUI.Slider(rect.AlignCenter(rect.width * 0.3f), "S", s, 0f, 1f);
        v = EditorGUI.Slider(rect.AlignRight(rect.width * 0.3f), "V", v, 0f, 1f);
        GUIHelper.PopLabelWidth();

        tempColor = Color.HSVToRGB(h, s, v);

        this.ValueEntry.SmartValue = tempColor;
    }
}
