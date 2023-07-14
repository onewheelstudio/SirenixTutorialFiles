using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class StatsUpgradeWindow : OdinEditorWindow
{
    [MenuItem("Tools/Stats Upgrade Creator")]
    private static void OpenWindow()
    {
        StatsUpgradeWindow window = GetWindow<StatsUpgradeWindow>();
        window.Show();
        window.path = PlayerPrefs.GetString("StatsPath", "");
    }
    private new void OnDestroy()
    {
        PlayerPrefs.SetString("StatsPath", path);
        base.OnDestroy();
    }

    [FolderPath, SerializeField, Required]
    private string path;

    [InlineEditor(Expanded = true)]
    public StatsUpgrade upgrade;

    [GUIColor(0.5f,1f,0.5f)]
    [ButtonGroup("")]
    private void SaveUpgrade()
    {
        upgrade.upgradeName = GenerateName();

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
        upgrade = ScriptableObject.CreateInstance<StatsUpgrade>();
        upgrade.cost = new HexGame.Resources.ResourceAmount(HexGame.Resources.ResourceType.Research, 500);
    }

    [GUIColor(0.5f, 1f, 0.5f)]
    [ButtonGroup("")]
    private string GenerateName()
    {
        string name;
        name = upgrade.unitsToUpgrade[0].name;
        name = name.Replace("Stats", "");

        foreach (var upgrade in upgrade.upgradeToApply)
        {
            if (upgrade.Value > 0)
                name += "Plus";
            else
                name += "Minus";

            name += $" {Mathf.Abs(upgrade.Value)} {upgrade.Key}";
        }

        return name;
    }
}
