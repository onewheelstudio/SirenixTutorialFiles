using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;

/// <summary>
/// This holds all the sprite data for the character
/// </summary>


[CreateAssetMenu(fileName = "New Character Sprites", menuName = "Create New Character Sprite SO")]
public class CharacterSpriteData : SerializedScriptableObject
{
    [LabelWidth(125)]
    public string characterName;

    //sprites
    [Title("Head")]
    [HorizontalGroup("Character", LabelWidth = 100)]
    [VerticalGroup("Character/Head")]
    [ValueDropdown("@GetBodySprites(spriteSettings.hatSprites)")]
    public Sprite hat;
    [VerticalGroup("Character/Head")]
    [ValueDropdown("@GetBodySprites(spriteSettings.hairSprites)")]
    public Sprite hair;
    [VerticalGroup("Character/Head")]
    [ValueDropdown("@GetBodySprites(spriteSettings.headSprites)")]
    public Sprite head;
    [VerticalGroup("Character/Head")]
    [ValueDropdown("@GetBodySprites(spriteSettings.faceSprites)")]
    public Sprite face;
    [VerticalGroup("Character/Head")]
    [ValueDropdown("@GetBodySprites(spriteSettings.beardSprites)")]
    public Sprite beard;

    [Title("Body")]
    [VerticalGroup("Character/Body")]
    [ValueDropdown("@GetBodySprites(spriteSettings.bodySprites)")]
    public Sprite body;
    [VerticalGroup("Character/Body")]
    [ValueDropdown("@GetBodySprites(spriteSettings.rightHandSprites)")]
    public Sprite rightHand;
    [VerticalGroup("Character/Body")]
    [ValueDropdown("@GetBodySprites(spriteSettings.leftHandSprites)")]
    public Sprite leftHand;
    [VerticalGroup("Character/Body")]
    [ValueDropdown("@GetBodySprites(spriteSettings.rightLegSprites)")]
    public Sprite rightLeg;
    [VerticalGroup("Character/Body")]
    [ValueDropdown("@GetBodySprites(spriteSettings.leftLegSprites)")]
    public Sprite leftLeg;


    [Title("Weapons")]
    [VerticalGroup("Character/Weapons")]
    [ValueDropdown("@GetBodySprites(spriteSettings.weaponSprites)")]
    public Sprite rightHandItem;
    [VerticalGroup("Character/Weapons")]
    [ValueDropdown("@GetBodySprites(spriteSettings.weaponSprites)")]
    public Sprite leftHandItem;

    private static CharacterSpriteSettings spriteSettings;

    //I'm leaving this in as a simpler visualization option
    //it also shows up one more Odin feature 

    //[SerializeField]
    //[TableMatrix(SquareCells = true)]
    //private Sprite[,] spriteArray;

    //private void OnValidate()
    //{
    //    DrawCharacterInArray();
    //}

    //[OnInspectorInit]
    //public void DrawCharacterInArray()
    //{
    //    spriteArray = new Sprite[5, 4]
    //    {
    //        {null,null,rightHandItem,null},
    //        {null,beard,rightHand,rightLeg},
    //        {hat,face,body,null},
    //        {null,head,leftHand,leftLeg},
    //        {null,null,leftHandItem,null}
    //    };
    //}

    [OnInspectorInit]
    private void GetSpriteSettings()
    {
        if (spriteSettings == null)
            spriteSettings = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Scripts/2D Character Creator/SpriteSettings.asset", typeof(CharacterSpriteSettings)) as CharacterSpriteSettings;
    }

    private IEnumerable<ValueDropdownItem> GetBodySprites(List<Sprite> spriteList)
    {
        List<ValueDropdownItem> dropDownItemList = new List<ValueDropdownItem>();

        dropDownItemList.Add(new ValueDropdownItem("none", null)); //adding a null value to dropdown

        for (int i = 0; i < spriteList.Count; i++)
        {
            ValueDropdownItem dropdownItem = new ValueDropdownItem(spriteList[i].name + " " + i, spriteList[i]);
            dropDownItemList.Add(dropdownItem);
        }

        return dropDownItemList;
    }

    [OnInspectorGUI]
    private void DrawPreview()
    {
        var rect = GUILayoutUtility.GetRect(300, 200);
        Rect newRect;

        if (rightHand != null) //all the if's seem horrible
        {
            newRect = PositionSprite(rect, -10, 120, rightHand);
            GUI.DrawTexture(newRect, rightHand.texture, ScaleMode.ScaleToFit);
        }

        if (leftLeg != null)
        {
            newRect = PositionSprite(rect, 10, 150, leftLeg);
            GUI.DrawTexture(newRect, leftLeg.texture, ScaleMode.ScaleToFit);
        }

        if (rightLeg != null)
        {
            newRect = PositionSprite(rect, -10, 150, rightLeg);
            GUI.DrawTexture(newRect, rightLeg.texture, ScaleMode.ScaleToFit);
        }

        if (body != null)
        {
            newRect = PositionSprite(rect, -5, 135, body);
            GUI.DrawTexture(newRect, body.texture, ScaleMode.ScaleToFit);
        }

        if (head != null)
        {
            newRect = PositionSprite(rect, 0, 105, head);
            GUI.DrawTexture(newRect, head.texture, ScaleMode.ScaleToFit);
        }

        if (face != null)
        {
            newRect = PositionSprite(rect, 0, 65, face);
            GUI.DrawTexture(newRect, face.texture, ScaleMode.ScaleToFit);
        }

        if (hair != null)
        {
            newRect = PositionSprite(rect, 0, 100, hair);
            GUI.DrawTexture(newRect, hair.texture, ScaleMode.ScaleToFit);
        }

        if (hat != null)
        {
            newRect = PositionSprite(rect, 0, 105, hat);
            GUI.DrawTexture(newRect, hat.texture, ScaleMode.ScaleToFit);
        }

        if (leftHand != null)
        {
            newRect = PositionSprite(rect, 10, 120, leftHand);
            GUI.DrawTexture(newRect, leftHand.texture, ScaleMode.ScaleToFit);
        }

        if (rightHandItem != null)
        {
            newRect = PositionSprite(rect, -30, 135, rightHandItem);
            GUI.DrawTexture(newRect, rightHandItem.texture, ScaleMode.ScaleToFit);
        }

        if (leftHandItem != null)
        {
            newRect = PositionSprite(rect, 12, 145, leftHandItem);
            GUI.DrawTexture(newRect, leftHandItem.texture, ScaleMode.ScaleToFit);
        }
    }

    private Rect PositionSprite(Rect rect, float x, float y, Sprite sprite)
    {
        float width = sprite.texture.width;
        float height = sprite.texture.height;

        float xPos = rect.x + x - sprite.pivot.x;//no width needed?
        xPos += rect.width / 2; //centering
        float yPos = rect.y + y + sprite.pivot.y - height; //why does the height matter?

        return new Rect(xPos, yPos, width, height);
    }
}
