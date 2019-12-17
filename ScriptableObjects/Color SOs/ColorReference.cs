using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
[InlineProperty]
public class ColorReference
{
    [HorizontalGroup("Color Reference",MaxWidth = 100)]
    [ValueDropdown("valueList")]
    [HideLabel]
    public bool useValue = true;

    [ShowIf("useValue", Animate = false)]
    [HorizontalGroup("Color Reference")]
    [HideLabel]
    public Color constantValue;

    [HideIf("useValue", Animate = false)]
    [HorizontalGroup("Color Reference")]
    [HideLabel]
    [Required]
    public ColorData variable;

    private ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
    {
        {"Value", true },
        {"Reference",false },
    };

    public Color Value
    {
        get
        {
            return useValue ? constantValue : variable.colorValue;
        }
        set
        {
            if (useValue)
                constantValue = value;
            else
                variable.colorValue = value;
        }



    }

    

}
