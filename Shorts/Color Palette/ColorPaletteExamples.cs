using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ColorPaletteExamples : MonoBehaviour
{
    [OnValueChanged("AssignColor")]
    [ColorPalette(PaletteName = "MyColors")]
    public Color myColor;

    private SpriteRenderer mySprite;

    //[OnInspectorGUI]
    private void AssignColor()
    {
        if (mySprite == null)
            mySprite = this.GetComponent<SpriteRenderer>();
        
        mySprite.color = myColor;

        //force update in scene view
        UnityEditor.SceneView.RepaintAll();
    }
}
