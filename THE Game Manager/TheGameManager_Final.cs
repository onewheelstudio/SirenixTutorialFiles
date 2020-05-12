using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;

public class TheGameManager_Final : MonoBehaviour
{
    //[MenuItem("Tools/The Game Manager Final")]
    public static void OpenWindow()
    {
        GetWindow<TheGameManager_Final>().Show();
    }

    [OnValueChanged("StateChange")]
    [LabelText("Manager View")]
    [LabelWidth(100f)]
    [EnumToggleButtons]
    [ShowInInspector]
    private ManagerState managerState; //used to control what is shown in the editor window
    private bool reBuildTree = false;

    private DrawUniverse drawUniverse = new DrawUniverse();
    private DrawNPC drawNPC = new DrawNPC();
    private DrawSFX drawSFX = new DrawSFX();

    //these are all scriptable object based
    private DrawSelected<ModuleData> drawModules = new DrawSelected<ModuleData>();
    private DrawSelected<ColorData> drawColors = new DrawSelected<ColorData>();
    private DrawSelected<OpenInventory.ItemData> drawItem = new DrawSelected<OpenInventory.ItemData>();
    private DrawSelected<RecipeBase> drawRecipe = new DrawSelected<RecipeBase>();

    //paths to SOs in project
    private string modulePath = "Assets/Prefabs/Ships/Module/ModuleData";
    private string colorPath = "Assets/Resources/ColorData";
    private string itemPath = "Assets/OpenInventory/Databases/Items";
    private string recipePath = "Assets/Scripts/Industry/Recipe Data";

    //set the path for each state so that new SOs of that type can be created
    protected override void Initialize()
    {
        drawModules.SetPath(modulePath);
        drawColors.SetPath(colorPath);
        drawItem.SetPath(itemPath);
        drawRecipe.SetPath(recipePath);

        drawUniverse.FindMyObject();
        drawNPC.FindMyObject();
        drawSFX.FindMyObject();
    }

    //called when the enum "manager state" is changed
    //might need more in here for later additions?
    private void StateChange()
    {
        reBuildTree = true;
    }

    //used to place title and enum buttons (Target 0) above Odin Menu Tree
    //only used in some windows
    protected override void OnGUI()
    {
        //update menu tree on type change
        if (reBuildTree && Event.current.type == EventType.Layout)
        {
            ForceMenuTreeRebuild();
            reBuildTree = false;
        }

        SirenixEditorGUI.Title("The Game Manager", "Because Every Hobby Game is Overscoped", TextAlignment.Center, true);
        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUILayout.Space();

        switch (managerState)
        {
            case ManagerState.modules:
            case ManagerState.Items:
            case ManagerState.Recipes:
            case ManagerState.color:
                DrawEditor(7);
                break;
            default:
                break;
        }

        UnityEditor.EditorGUILayout.Space();
        base.OnGUI();
    }

    //targets are separate classes that wrap the main class
    //the idea here was to allow the addition of buttons and other functions
    //for use in the editor window that aren't needed in the class itself
    protected override IEnumerable<object> GetTargets()
    {
        List<object> targets = new List<object>();
        targets.Add(drawUniverse); //target 0
        targets.Add(drawModules); //target 1
        targets.Add(drawNPC);//target 2
        targets.Add(drawItem); //target 3
        targets.Add(drawRecipe); //target 4
        targets.Add(drawColors); //target 5
        targets.Add(drawSFX); //target 6
        targets.Add(base.GetTarget()); //target 7

        return targets;
    }

    protected override void DrawEditors()
    {
        switch (managerState)
        {
            case ManagerState.Universe:
                DrawEditor(7);
                break;
            case ManagerState.modules:
                drawModules.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.NPC:
                DrawEditor(7);
                break;
            case ManagerState.Items:
                drawItem.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.Recipes:
                drawRecipe.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.color:
                drawColors.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            case ManagerState.SFX:
                DrawEditor(7);
                break;
            default:
                break;
        }

        //draw editor based on enum value
        DrawEditor((int)managerState);
    }

    //control over Odin Menu Tree
    protected override void DrawMenu()
    {
        switch (managerState)
        {
            case ManagerState.modules:
            case ManagerState.Items:
            case ManagerState.Recipes:
            case ManagerState.color:
                base.DrawMenu();
                break;
            default:
                break;
        }
    }

    //I'm building multiple trees depending on what "state" is selected
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        switch (managerState)
        {
            case ManagerState.modules:
                tree.AddAllAssetsAtPath("Module Data", modulePath, typeof(ModuleData));
                break;
            case ManagerState.Items:
                tree.AddAllAssetsAtPath("Item Data", itemPath, typeof(OpenInventory.ItemData));
                break;
            case ManagerState.Recipes:
                tree.AddAllAssetsAtPath("Reciped", recipePath, typeof(RecipeBase));
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
        Universe,
        modules,
        NPC,
        Items,
        Recipes,
        color,
        SFX
    }
}

//Used to draw the current object that is selected in the Menu Tree
//Look at me using generics ;)
public class DrawSelected<T> where T : ScriptableObject
{
    //[Title("@property.name")]
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public T selected;

    [LabelWidth(100)]
    [PropertyOrder(-1)]
    [ColorGroupAttribute("CreateNew", 1f, 1f, 1f)]
    [HorizontalGroup("CreateNew/Horizontal")]
    public string nameForNew;

    private string path;

    [HorizontalGroup("CreateNew/Horizontal")]
    [GUIColor(0.7f, 0.7f, 1f)]
    [Button]
    public void CreateNew()
    {
        if (nameForNew == "")
            return;

        T newItem = ScriptableObject.CreateInstance<T>();
        newItem.name = "New " + typeof(T).ToString();

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
        if (selected != null)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }
    }

    public void SetSelected(object item)
    {
        //ensure selection is of the correct type
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
    [GUIColor(0.7f, 1f, 0.7f)]
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
        newManager.name = "new " + typeof(T).ToString();
        myObject = newManager.AddComponent<T>();
    }
}

//used to wrap the "Universe Generator" class to allow the addition of buttons
//and other functionality that the actual generator script does not need
//but that could be useful in the editor window
public class DrawUniverse : DrawSceneObject<UniverseCreator>
{
    [ShowIf("@myObject != null")]
    [GUIColor(0.7f, 1f, 1f)]
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



