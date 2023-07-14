using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class UnitUnlocakUpgradeWindow : OdinEditorWindow
{
    [MenuItem("Tools/Unlock Unit Upgrade Creator")]
    private static void OpenWindow()
    {
        UnitUnlocakUpgradeWindow window = GetWindow<UnitUnlocakUpgradeWindow>();
        window.Show();
        window.path = PlayerPrefs.GetString("UnlockUnitPath", "");
    }

    private new void OnDestroy()
    {
        PlayerPrefs.SetString("UnlockUnitPath", path);
        base.OnDestroy();
    }

    [FolderPath, SerializeField, Required]
    private string path;

    [InlineEditor(Expanded = true)]
    public UnitUnlockUpgrade upgrade;

    [GUIColor(0.5f,1f,0.5f)]
    [ButtonGroup("")]
    private void SaveUpgrade()
    {
        upgrade.upgradeName = GenerateName();
        upgrade.description = GenerateDescription();

        if (string.IsNullOrEmpty(upgrade.upgradeName))
            return;

        AssetDatabase.CreateAsset(upgrade, path + "/" + upgrade.upgradeName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        NewUpgrade();
    }
    
    [GUIColor(0.5f,0.5f,1f)]
    [ButtonGroup("")]
    private void NewUpgrade()
    {
        upgrade = ScriptableObject.CreateInstance<UnitUnlockUpgrade>();
        upgrade.cost = new HexGame.Resources.ResourceAmount(HexGame.Resources.ResourceType.Research, 500);
    }

    [GUIColor(0.5f, 1f, 0.5f)]
    [ButtonGroup("")]
    private string GenerateName()
    {
        string name = "Unlock ";

        name += upgrade.buildingsToUnlock[0].ToNiceString();

        return name;
    }

    private string GenerateDescription()
    {
        return $"Unlocks {upgrade.buildingsToUnlock[0].ToNiceString()}";
    }
}
