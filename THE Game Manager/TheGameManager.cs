using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class TheGameManager : OdinMenuEditorWindow
{
    [OnValueChanged("StateChange")]
    [LabelText("Manager View")]
    [LabelWidth(100f)]
    [EnumToggleButtons]
    [ShowInInspector]
    private ManagerState managerState;
    private int enumIndex = 0;
    private bool treeRebuild = false;

    private DrawUniverse drawUniverse = new DrawUniverse();
    private DrawNPC drawNPC = new DrawNPC();
    private DrawSFX drawSFX = new DrawSFX();

    private DrawSelected<ModuleData> drawModules = new DrawSelected<ModuleData>();
    private DrawSelected<ColorData> drawColors = new DrawSelected<ColorData>();
    private DrawSelected<OpenInventory.ItemData> drawItems = new DrawSelected<OpenInventory.ItemData>();
    private DrawSelected<RecipeBase> drawRecipes = new DrawSelected<RecipeBase>();

    //paths to SOs in project
    private string modulePath = "Assets/Prefabs/Ships/Module/ModuleData";
    private string colorPath = "Assets/Resources/ColorData";
    private string itemPath = "Assets/Resources/Items";
    private string recipePath = "Assets/Scripts/Industry/Recipe Data";

    [MenuItem("Tools/The Game Manager")]
    public static void OpenWindow()
    {
        GetWindow<TheGameManager>().Show();
    }

    private void StateChange()
    {
        treeRebuild = true;
    }

    protected override void Initialize()
    {
        drawModules.SetPath(modulePath);
        drawColors.SetPath(colorPath);
        drawItems.SetPath(itemPath);
        drawRecipes.SetPath(recipePath);

        drawUniverse.FindMyObject();
        drawNPC.FindMyObject();
        drawSFX.FindMyObject();
    }

    protected override void OnGUI()
    {
        if(treeRebuild && Event.current.type == EventType.Layout)
        {
            ForceMenuTreeRebuild();
            treeRebuild = false;
        }

        SirenixEditorGUI.Title("The Game Manager", "Because every hobby game is overscoped", TextAlignment.Center, true);
        EditorGUILayout.Space();

        switch (managerState)
        {

            case ManagerState.modules:
            case ManagerState.items:
            case ManagerState.recipes:
            case ManagerState.color:
                DrawEditor(enumIndex);
                break;
            default:
                break;
        }
        EditorGUILayout.Space();

        base.OnGUI();
    }

    protected override void DrawEditors()
    {
        switch (managerState)
        {
            case ManagerState.universe:
                DrawEditor(enumIndex);
                break;
            case ManagerState.modules:
                drawModules.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.npc:
                DrawEditor(enumIndex);
                break;
            case ManagerState.items:
                drawItems.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.recipes:
                drawRecipes.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.color:
                drawColors.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.sfx:
                DrawEditor(enumIndex);
                break;
            default:
                break;
        }

        DrawEditor((int)managerState);
    }

    protected override IEnumerable<object> GetTargets()
    {
        List<object> targets = new List<object>();
        targets.Add(drawUniverse);
        targets.Add(drawModules);
        targets.Add(drawNPC);
        targets.Add(drawItems);
        targets.Add(drawRecipes);
        targets.Add(drawColors);
        targets.Add(drawSFX);
        targets.Add(base.GetTarget());

        enumIndex = targets.Count - 1;

        return targets;
    }

    protected override void DrawMenu()
    {
        switch (managerState)
        {

            case ManagerState.modules:
            case ManagerState.items:
            case ManagerState.recipes:
            case ManagerState.color:
                base.DrawMenu();
                break;
            default:
                break;
        }
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new OdinMenuTree();

        switch (managerState)
        {

            case ManagerState.modules:
                tree.AddAllAssetsAtPath("Module Data", modulePath, typeof(ModuleData));
                break;
            case ManagerState.items:
                tree.AddAllAssetsAtPath("Item Data", itemPath, typeof(OpenInventory.ItemData));
                break;
            case ManagerState.recipes:
                tree.AddAllAssetsAtPath("Recipes", recipePath, typeof(RecipeBase));
                break;
            case ManagerState.color:
                tree.AddAllAssetsAtPath("Color Data", colorPath, typeof(ColorData));
                break;
            default:
                break;
        }

        return tree;
    }

    public enum ManagerState
    {
        universe,
        modules,
        npc,
        items,
        recipes,
        color,
        sfx
    }
}

public class DrawSelected<T> where T : ScriptableObject
{
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public T selected;

    [LabelWidth(100)]
    [PropertyOrder(-1)]
    [ColorGroupAttribute("CreateNew", 1f,1f,1f)]
    [HorizontalGroup("CreateNew/Horizontal")]
    public string nameForNew;

    private string path;

    [HorizontalGroup("CreateNew/Horizontal")]
    [GUIColor(0.7f,0.7f,1f)]
    [Button]
    public void CreateNew()
    {
        if (nameForNew == "")
            return;

        T newItem = ScriptableObject.CreateInstance<T>();
        //newItem.name = "New " + typeof(T).ToString();

        if (path == "")
            path = "Assets/";

        AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
        AssetDatabase.SaveAssets();

        nameForNew = "";
    }

    [HorizontalGroup("CreateNew/Horizontal")]
    [GUIColor(1f, 0.7f, 0.7f)]
    [Button]
    public void DeleteSelected()
    {
        if(selected != null)
        {
            string _path = AssetDatabase.GetAssetPath(selected);
            AssetDatabase.DeleteAsset(_path);
            AssetDatabase.SaveAssets();
        }
    }

    public void SetSelected(object item)
    {
        var attempt = item as T;
        if (attempt != null)
            this.selected = attempt;
    }

    public void SetPath(string path)
    {
        this.path = path;
    }
}

public class DrawSceneObject<T> where T : MonoBehaviour
{
    [Title("Universe Creator")]
    [ShowIf("@myObject != null")]
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public T myObject;

    public void FindMyObject()
    {
        if (myObject == null)
            myObject = GameObject.FindObjectOfType<T>();
    }

    [ShowIf("@myObject != null")]
    [GUIColor(0.7f,1f,0.7f)]
    [ButtonGroup("Top Button", -1000)]
    private void SelectSceneObject()
    {
        if (myObject != null)
            Selection.activeGameObject = myObject.gameObject;
    }

    [ShowIf("@myObject == null")]
    [Button]
    private void CreateManagerObject()
    {
        GameObject newManager = new GameObject();
        newManager.name = "New " + typeof(T).ToString();
        myObject = newManager.AddComponent<T>();
    }
}

public class DrawUniverse : DrawSceneObject<UniverseCreator>
{
    [ShowIf("@myObject != null")]
    [GUIColor(0.7f,1f,1f)]
    [ButtonGroup("Top Button")]
    private void SomeUniverseFunction1()
    {

    }

    [ShowIf("@myObject != null")]
    [GUIColor(0.7f, 0.7f, 1f)]
    [ButtonGroup("Top Button")]
    private void SomeUniverseFunction2()
    {

    }
}

public class DrawNPC : DrawSceneObject<NPCManager>
{

}

public class DrawSFX : DrawSceneObject<SFXManager>
{

}