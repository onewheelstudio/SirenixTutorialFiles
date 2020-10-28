using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ColorIfExample : MonoBehaviour
{
    [ColorIf("@Color.green", "HasEvenNumberOfCharacters")]
    public string greenIfEven;

    private bool HasEvenNumberOfCharacters()
    {
        return greenIfEven?.Length % 2 == 0;
    }
}


