using System;

public class ColorIfAttribute : Attribute
{
    public string color;
    public string condition;
    public string myString;

    public ColorIfAttribute(string color, string condition)
    {
        this.color = color;
        this.condition = condition;
    }
}

//public class ColorIfAttributeDrawer : OdinAttributeDrawer<ColorIfAttribute>
//{
//    private ValueResolver<Color> colorResolver;
//    private ValueResolver<bool> conditionResolver;

//    protected override void Initialize()
//    {
//        this.colorResolver = ValueResolver.Get<Color>(this.Property, this.Attribute.color, Color.white);
//        this.conditionResolver = ValueResolver.Get<bool>(this.Property, this.Attribute.condition);
//    }

//    protected override void DrawPropertyLayout(GUIContent label)
//    {
//        ValueResolver.DrawErrors(this.colorResolver, this.conditionResolver);

//        bool condition = this.conditionResolver.GetValue();

//        if (condition)
//        {
//            GUIHelper.PushColor(this.colorResolver.GetValue());
//        }

//        this.CallNextDrawer(label);

//        if (condition)
//        {
//            GUIHelper.PopColor();
//        }
//    }
//}