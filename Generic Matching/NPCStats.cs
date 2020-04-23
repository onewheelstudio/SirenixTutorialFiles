using UnityEngine;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

[System.Serializable]
public class BaseStats : IResettable
{
    public int health;
    public int gold;

    public void Reset()
    {
        health = 100;
        gold = 100;
    }
}

[System.Serializable]
public class MagicStats : BaseStats
{
    public int mana;
    public int manaRecharge;
}

public interface IResettable
{
    void Reset();
}

public class AddBoxToStatsDrawer<T> : OdinValueDrawer<T> where T : BaseStats
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        SirenixEditorGUI.BeginBox();
        this.CallNextDrawer(label);
        SirenixEditorGUI.EndBox();
    }
}

public class ResetContextMenuDrawer<T> : OdinValueDrawer<T>, IDefinesGenericMenuItems where T : IResettable
{
    protected override void Initialize()
    {
        this.SkipWhenDrawing = true;
    }

    public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
    {
        genericMenu.AddItem(new GUIContent("Reset Stats"), false, Reset);
    }

    private void Reset()
    {
        this.Property.RecordForUndo("Reset Stats with IResettable");

        foreach (var value in this.ValueEntry.Values)
        {
            value.Reset();
        }
    }
}

public class ResetListDrawer<TList, TElement> : OdinValueDrawer<TList>, IDefinesGenericMenuItems where TList : IList<TElement>
    where TElement : IResettable
{

    protected override void Initialize()
    {
        this.SkipWhenDrawing = true;
    }

    public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
    {
        genericMenu.AddItem(new GUIContent("Reset List"), false, ResetList);
    }

    private void ResetList()
    {
        this.Property.RecordForUndo("Reset List with IResettable");

        foreach (var list in this.ValueEntry.Values)
        {
            foreach (var element in list)
            {
                element.Reset();
            }
        }
    }
}