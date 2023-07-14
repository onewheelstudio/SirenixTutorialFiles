using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using HexGame.Resources;
using System.Collections.Generic;
using System.Linq;

public class ResourceSOCreator : OdinEditorWindow
{
    [MenuItem("Tools/Resource Creator")]
    private static void OpenWindow()
    {
        ResourceSOCreator window = GetWindow<ResourceSOCreator>();
        window.Show();
        window.path = PlayerPrefs.GetString("Resource SO Path", "");
    }
    private new void OnDestroy()
    {
        PlayerPrefs.SetString("Resource SO Path", path);
        base.OnDestroy();
    }

    [SerializeField]
    [OnValueChanged("FindTemplate")]
    private ResourceType resourceType;

    [FolderPath, SerializeField, Required]
    private string path;

    [InlineEditor(Expanded = true)]
    public ResourceTemplate resource;

    [GUIColor(0.5f,1f,0.5f)]
    [ButtonGroup("")]
    private void SaveResourceTemplate()
    {
        if (string.IsNullOrEmpty(resource.resourceName))
            return;

        if(!AssetDatabase.Contains(resource))
            AssetDatabase.CreateAsset(resource, path + "/" + resource.resourceName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        NewUpgrade();
    }
    
    [GUIColor(0.5f,0.5f,1f)]
    [ButtonGroup("")]
    private void NewUpgrade()
    {
        resource = ScriptableObject.CreateInstance<ResourceTemplate>();
    }

    [GUIColor(0.5f, 1f, 0.5f)]
    [ButtonGroup("")]
    private void CreateRemainingTypes()
    {
        List<ResourceTemplate> templates = HelperFunctions.GetScriptableObjects<ResourceTemplate>(path);

        foreach (var rt in System.Enum.GetValues(typeof(ResourceType)))
        {
            ResourceTemplate resourceTemplate = templates.Where(x => x.type == (ResourceType)rt).FirstOrDefault();
            if(resourceTemplate == null)
            {
                NewUpgrade();
                resource.type = (ResourceType)rt;
                SaveResourceTemplate();
            }
        }
    }

    private void FindTemplate()
    {
        if (resource != null)
            SaveResourceTemplate();

        Debug.Log("Looking for template");
        List<ResourceTemplate> templates = HelperFunctions.GetScriptableObjects<ResourceTemplate>(path);
        ResourceTemplate resourceTemplate = null;

        if(templates != null && templates.Count > 0)
            resourceTemplate = templates.Where(x => x.type == resourceType).FirstOrDefault();

        if(resourceTemplate == null)
        {
            NewUpgrade();
            resource.type = (ResourceType)resourceType;
        }
        else
        {
            resource = resourceTemplate;
        }
    }
}
