using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class CharacterEditor : OdinMenuEditorWindow
{
    private static CharacterSpriteSettings sprites;

    [MenuItem("Tools/2D Character Creator")]
    public static void OpenWindow()
    {
        GetWindow<CharacterEditor>().Show();
        if (sprites == null)
            sprites = AssetDatabase.LoadAssetAtPath("Assets/Scripts/2D Character Creator/SpriteSettings.asset", typeof(CharacterSpriteSettings)) as CharacterSpriteSettings;
    }
     
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewCharcacterData());
        tree.AddAllAssetsAtPath("Enemy Data", "Assets/Scripts/2D Character Creator/Characters", typeof(CharacterSpriteData));
        if(sprites != null)
            tree.Add("Set Up Sprites", sprites);
        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        OdinMenuTreeSelection selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
                CharacterSpriteData asset = selected.SelectedValue as CharacterSpriteData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    public class CreateNewCharcacterData
    {
        public CreateNewCharcacterData()
        {
            characterSpriteData = ScriptableObject.CreateInstance<CharacterSpriteData>();
            characterSpriteData.characterName = "New Character Data";
        }

        [InlineEditor(Expanded = true)]
        public CharacterSpriteData characterSpriteData;

        [GUIColor(0.7f,1f,0.7f)]
        [ButtonGroup("CreateButtons")]
        private void CreateCharacterData()
        {
            AssetDatabase.CreateAsset(characterSpriteData, "Assets/Scripts/2D Character Creator/Characters/" + characterSpriteData.characterName + ".asset");
            AssetDatabase.SaveAssets();
        }

        [GUIColor(0.7f,0.7f,1f)]
        [ButtonGroup("CreateButtons")]
        private void RandomizeSprites()
        {
            CharacterSpriteSettings sprites = AssetDatabase.LoadAssetAtPath("Assets/Scripts/2D Character Creator/SpriteSettings.asset", typeof(CharacterSpriteSettings)) as CharacterSpriteSettings;

            if (sprites == null)
                return;

            characterSpriteData.body = sprites.bodySprites[Random.Range(0, sprites.bodySprites.Count)];
            characterSpriteData.head = sprites.headSprites[Random.Range(0, sprites.headSprites.Count)];
            characterSpriteData.face = sprites.faceSprites[Random.Range(0, sprites.faceSprites.Count)];
            characterSpriteData.beard = sprites.beardSprites[Random.Range(0, sprites.beardSprites.Count)];
            characterSpriteData.hair = sprites.hairSprites[Random.Range(0, sprites.hairSprites.Count)];
            characterSpriteData.hat = sprites.hatSprites[Random.Range(0, sprites.hatSprites.Count)];
            characterSpriteData.rightHand = sprites.rightHandSprites[Random.Range(0, sprites.rightHandSprites.Count)];
            characterSpriteData.rightHandItem = sprites.weaponSprites[Random.Range(0, sprites.weaponSprites.Count)];
            characterSpriteData.leftHand = sprites.leftHandSprites[Random.Range(0, sprites.leftHandSprites.Count)];
            characterSpriteData.leftHandItem = sprites.weaponSprites[Random.Range(0, sprites.weaponSprites.Count)];
            characterSpriteData.rightLeg = sprites.rightLegSprites[Random.Range(0, sprites.rightLegSprites.Count)];
            characterSpriteData.leftLeg = sprites.leftLegSprites[Random.Range(0, sprites.leftLegSprites.Count)];
        }
    }
}



