using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;

public class MyClass : MonoBehaviour
{


    [ColorFoldoutGroup("group1", 1f,0f,0f)]
    public int first = 1;
    [ColorFoldoutGroup("group1")]
    public int second = 2;
    [ColorFoldoutGroup("group1")]
    public int third = 3;

    [ColorFoldoutGroup("group2", 0f, 1f, 0f)]
    public string down;
    [ColorFoldoutGroup("group2")]
    public string up;
    [ColorFoldoutGroup("group2")] 
    public string strange;

    [ColorFoldoutGroup("group3", 0f, 0f, 0.7f)]
    public Vector3 charm = new Vector3(1,0,0);
    [ColorFoldoutGroup("group3")]
    public Vector3 bottom = new Vector3(0, -1, 0);
    [ColorFoldoutGroup("group3")]
    public Vector3 top = new Vector3(0, 0, 0);
}

public class ColorFoldoutGroupAttribute : PropertyGroupAttribute
{
    public float R, G, B, A;

    public ColorFoldoutGroupAttribute(string path) : base (path)
    {

    }

    public ColorFoldoutGroupAttribute(string path, float r, float g, float b, float a = 1f) : base(path)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttr = (ColorFoldoutGroupAttribute)other;

        this.R = Math.Max(otherAttr.R, this.R);
        this.G = Math.Max(otherAttr.G, this.G);
        this.B = Math.Max(otherAttr.B, this.B);
        this.A = Math.Max(otherAttr.A, this.A);
    }
}

public class ColorFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColorFoldoutGroupAttribute>
{
    private LocalPersistentContext<bool> isExpanded;

    protected override void Initialize()
    {
        this.isExpanded = this.GetPersistentValue<bool>("ColorFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        GUIHelper.PopColor();

        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);
        SirenixEditorGUI.EndBoxHeader();

        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }
        }
        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox();
    }
}