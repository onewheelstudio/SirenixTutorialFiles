using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
public class ColorIfAttributeDrawer : OdinAttributeDrawer<ColorIfAttribute>
{
    private ValueResolver<Color> colorResolver;
    private ValueResolver<bool> conditionResolver;

    protected override void Initialize()
    {
        this.colorResolver = ValueResolver.Get<Color>(this.Property, this.Attribute.color, Color.white);
        this.conditionResolver = ValueResolver.Get<bool>(this.Property, this.Attribute.condition);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        ValueResolver.DrawErrors(this.colorResolver, this.conditionResolver);

        bool condition = this.conditionResolver.GetValue();

        if(condition)
        {
            GUIHelper.PushColor(this.colorResolver.GetValue());
        }

        this.CallNextDrawer(label);

        if(condition)
        {
            GUIHelper.PopColor();
        }
    }
}


