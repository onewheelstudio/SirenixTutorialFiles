using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ActionResolvers;
using UnityEngine;

public class ActionButtonAttributeDrawer : OdinAttributeDrawer<ActionButtonAttribute>
{
    private ActionResolver actionResolver;

    protected override void Initialize()
    {
        this.actionResolver = ActionResolver.Get(this.Property, this.Attribute.action);
    }

    protected override void DrawPropertyLayout (GUIContent label)
    {
        this.actionResolver.DrawError();

        if(GUILayout.Button("Perform Action"))
        {
            this.actionResolver.DoActionForAllSelectionIndices();
        }

        this.CallNextDrawer(label);
    }
}


