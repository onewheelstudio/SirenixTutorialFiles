using UnityEngine;
using System;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(SpriteSizeValidator))]
public class SpriteSizeValidator : AttributeValidator<SpriteSizeAttribute, Sprite>
{
    protected override void Validate(ValidationResult result)
    {
        if (this.ValueEntry.SmartValue == null)
            return;

        int size = this.Attribute.size;
        int width = this.ValueEntry.SmartValue.texture.width;

        if (width != size)
        {
            result.ResultType = ValidationResultType.Warning;
            result.Message = "The Size of the sprite is NOT the desired size of " + width + " instead it is " + size;
        }
    }
}

public class SpriteSizeAttribute : Attribute
{
    public int size;
    
    public SpriteSizeAttribute(int size)
    {
        this.size = size;
    }

    public SpriteSizeAttribute(SpriteSize spriteSize)
    {
        this.size = (int)spriteSize;
    }
}

public enum SpriteSize
{
    small = 128,
    medium = 256,
    large = 512,
    extraLarge = 1024,
    huge = 2048
}


