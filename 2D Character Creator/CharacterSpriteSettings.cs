using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// This holds all the project wide sprite data
/// these fields can be accessed in the editor
/// and the Character Data SO
/// </summary>

[CreateAssetMenu(fileName = "SpriteSettings", menuName = "Create Sprite Settings SO")]
public class CharacterSpriteSettings : ScriptableObject
{
    [Title("Asset Locations")]
    [FolderPath]
    public List<string> spriteFolders;
    [FolderPath]
    public List<string> otherSpriteFolders;

    [Title("Head")]
    [SerializeField]
    private string headIdentifier;
    public List<Sprite> headSprites = new List<Sprite>();
    [SerializeField]
    private string faceIdentifier;
    public List<Sprite> faceSprites = new List<Sprite>();
    [SerializeField]
    private string beardIdentifier;
    public List<Sprite> beardSprites = new List<Sprite>();
    [SerializeField]
    private string hairIdentifier;
    public List<Sprite> hairSprites = new List<Sprite>();
    [SerializeField]
    private string hatIdentifier;
    public List<Sprite> hatSprites = new List<Sprite>();

    [Title("Body")]
    [SerializeField]
    private string bodyIdentifier;
    public List<Sprite> bodySprites = new List<Sprite>();
    [SerializeField]
    private string rightHandIdentifier;
    public List<Sprite> rightHandSprites = new List<Sprite>();
    [SerializeField]
    private string leftHandIdentifier;
    public List<Sprite> leftHandSprites = new List<Sprite>();
    [SerializeField]
    private string rightLegIdentifier;
    public List<Sprite> rightLegSprites = new List<Sprite>();
    [SerializeField]
    private string leftLegIdentifier;
    public List<Sprite> leftLegSprites = new List<Sprite>();

    [Title("Weapons")]
    [SerializeField]
    private string weaponIndentifier;
    [VerticalGroup("Character/Weapons")]
    public List<Sprite> weaponSprites = new List<Sprite>();

    [Button]
    public void FindSpritesInAssets()
    {
        if (spriteFolders == null || spriteFolders.Count == 0)
            return;

        FindSpritesAddToList(bodyIdentifier, bodySprites);
        FindSpritesAddToList(headIdentifier, headSprites);
        FindSpritesAddToList(faceIdentifier, faceSprites);
        FindSpritesAddToList(beardIdentifier, beardSprites);
        FindSpritesAddToList(hairIdentifier, hairSprites);
        FindSpritesAddToList(hatIdentifier, hatSprites);
        FindSpritesAddToList(rightHandIdentifier, rightHandSprites);
        FindSpritesAddToList(leftHandIdentifier, leftHandSprites);
        FindSpritesAddToList(rightLegIdentifier, rightLegSprites);
        FindSpritesAddToList(leftLegIdentifier, leftLegSprites);

        if (otherSpriteFolders == null || otherSpriteFolders.Count == 0)
            return;

        FindUnidentifiedSpritesAddToList(weaponSprites);
    }

    private void FindSpritesAddToList(string identifier, List<Sprite> spriteList)
    {
        spriteList.Clear();
        string[] folders = spriteFolders.ToArray();
        string[] spriteGUIDs = UnityEditor.AssetDatabase.FindAssets(identifier + " t:sprite", folders);

        foreach (string GUID in spriteGUIDs)
        {
            string spritePath = UnityEditor.AssetDatabase.GUIDToAssetPath(GUID);
            Sprite foundSprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

            if (foundSprite != null)
                spriteList.Add(foundSprite);
        }
    }

    private void FindUnidentifiedSpritesAddToList(List<Sprite> spriteList)
    {
        spriteList.Clear();
        string[] folders = otherSpriteFolders.ToArray();
        string[] spriteGUIDs = UnityEditor.AssetDatabase.FindAssets("t:sprite", folders);

        foreach (string GUID in spriteGUIDs)
        {
            string spritePath = UnityEditor.AssetDatabase.GUIDToAssetPath(GUID);
            Sprite foundSprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

            if (foundSprite != null)
                spriteList.Add(foundSprite);
        }
    }
}
