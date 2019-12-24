using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class EnemyDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Enemy Data")]
    private static void OpenWindow() 
    {
        GetWindow<EnemyDataEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewEnemyData());
        tree.AddAllAssetsAtPath("Enemy Data", "Assets/Scripts", typeof(EnemyData));
        return tree;
    }

    public class CreateNewEnemyData
    {
        public CreateNewEnemyData()
        {
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.enemyName = "New Enemy Data";
        }

        [InlineEditor(Expanded = true)]
        public EnemyData enemyData;

        [Button("Add New Enemy SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(enemyData, "Assets/Scripts/" + enemyData.enemyName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }

    protected override void OnBeginDrawEditors()
    {
        OdinMenuTreeSelection selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
                EnemyData asset = selected.SelectedValue as EnemyData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }

        }
        SirenixEditorGUI.EndHorizontalToolbar(); 
    }
}
