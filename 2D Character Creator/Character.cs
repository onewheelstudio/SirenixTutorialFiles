using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{
    [OnValueChanged("SetSprites")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout, Expanded = true)]
    [OnValueChanged("SetSprites", true)] //will only update if SO changed in character inspector
    [TabGroup("Sprites")]
    public CharacterSpriteData spriteData;

    //Sprite Renderers
    //these correspond to the sprites in the SO

    [Title("Head")]
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer head;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer face;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer beard;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer hair;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer hat;

    [Title("Body")]
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer body;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer rightHand;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer leftHand;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer rightLeg;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer leftLeg;

    [Title("Weapons")]
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer rightHandItem;
    [TabGroup("Sprite Renderers")]
    public SpriteRenderer leftHandItem;

    private void SetSprites()
    {
        if (spriteData == null)
            return;

        body.sprite = spriteData.body;
        head.sprite = spriteData.head;
        face.sprite = spriteData.face;
        beard.sprite = spriteData.beard;
        hair.sprite = spriteData.hair;
        hat.sprite = spriteData.hat;
        rightHand.sprite = spriteData.rightHand;
        rightHandItem.sprite = spriteData.rightHandItem;
        leftHand.sprite = spriteData.leftHand;
        leftHandItem.sprite = spriteData.leftHandItem;
        rightLeg.sprite = spriteData.rightLeg;
        leftLeg.sprite = spriteData.leftLeg;
    }
}
