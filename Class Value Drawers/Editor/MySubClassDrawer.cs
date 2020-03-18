using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;


public class MySubClassDrawer : OdinValueDrawer<MySubClass>
{
    private InspectorProperty text;
    private InspectorProperty number;
    private InspectorProperty location;

    protected override void Initialize()
    {
        text = this.Property.Children["text"];
        number = this.Property.Children["number"];
        location = this.Property.Children["location"];
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Rect rect = EditorGUILayout.GetControlRect();

        if (label != null)
            rect = EditorGUI.PrefixLabel(rect, label);

        rect = EditorGUILayout.GetControlRect();

        GUIHelper.PushLabelWidth(75);
        text.ValueEntry.WeakSmartValue = SirenixEditorFields.TextField(rect.Split(0, 2),
            "Text", (string)text.ValueEntry.WeakSmartValue);
        number.ValueEntry.WeakSmartValue = SirenixEditorFields.IntField(rect.Split(1, 2),
            "Number", (int)number.ValueEntry.WeakSmartValue);
        location.Draw();
        GUIHelper.PopLabelWidth();
    }
}

public class MySubClassDrawer_WrongWay : OdinValueDrawer<MySubClass>
{
    private InspectorProperty text;
    private InspectorProperty number;
    private InspectorProperty location;

    protected override void Initialize()
    {
        text = this.Property.Children["text"];
        number = this.Property.Children["number"];
        location = this.Property.Children["location"];
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        string text = this.ValueEntry.SmartValue.text;
        int number = this.ValueEntry.SmartValue.number;
        Vector3 location = this.ValueEntry.SmartValue.location;

        Rect rect = EditorGUILayout.GetControlRect();

        if (label != null)
            rect = EditorGUI.PrefixLabel(rect, label);

        rect = EditorGUILayout.GetControlRect();

        GUIHelper.PushLabelWidth(75);
        text = SirenixEditorFields.TextField(rect.Split(0, 2), "Text", text);
        number = SirenixEditorFields.IntField(rect.Split(1, 2), "Number", number);
        location = SirenixEditorFields.Vector3Field("Location", location);
        GUIHelper.PopLabelWidth();

        this.ValueEntry.SmartValue.text = text;
        this.ValueEntry.SmartValue.number = number;
        this.ValueEntry.SmartValue.location = location;
    }
}
