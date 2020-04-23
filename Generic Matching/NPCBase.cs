using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class NPCBase : MonoBehaviour
{
    public BaseStats stats;

    public MagicStats magicStats;

    public List<BaseStats> NPCStatsList = new List<BaseStats>(); 
}

//public interface IResettable
//{
//    void Reset();
//}

//public class AddBoxToStatsDrawer<T> : OdinValueDrawer<T> where T : BaseStats
//{
//    protected override void DrawPropertyLayout(GUIContent label)
//    {
//        SirenixEditorGUI.BeginBox();
//        this.CallNextDrawer(label);
//        SirenixEditorGUI.EndBox();
//    }
//}

//public class ResetContextMenuDrawer<T> : OdinValueDrawer<T>, IDefinesGenericMenuItems where T : IResettable
//{

//    protected override void Initialize()
//    {
//        this.SkipWhenDrawing = true;
//    }

//    public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
//    {
//        genericMenu.AddItem(new GUIContent("Reset Stats"), false, Reset);
//    }

//    private void Reset()
//    {
//        this.Property.RecordForUndo("Resetting values using IResettable");

//        foreach (var value in this.ValueEntry.Values)
//        {
//            value.Reset();
//        }
//    }
//}

//public class ResetListDrawr<TList, TElement> : OdinValueDrawer<TList>, IDefinesGenericMenuItems where TList : IList<TElement>
//    where TElement : IResettable
//{
//    protected override void Initialize()
//    {
//        this.SkipWhenDrawing = true;
//    }

//    public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
//    {
//        genericMenu.AddItem(new GUIContent("Reset List"), false, ResetList);
//    }

//    private void ResetList()
//    {
//        this.Property.RecordForUndo("Resetting List using IResettable");

//        foreach (var list in this.ValueEntry.Values)
//        {
//            foreach (var element in list)
//            {
//                element.Reset();
//            }
//        }
//    }
//}
