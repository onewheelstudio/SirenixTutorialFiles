using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System;
using System.Reflection;

public class CharacterAttributeProcessor<T> : OdinAttributeProcessor<T> where T : Character
{
    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        attributes.Add(new LabelWidthAttribute(110));

        if(member.Name == "icon")
        {
            attributes.Add(new PropertyOrderAttribute(-1));
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new HorizontalGroupAttribute("Character", 100));
            attributes.Add(new PreviewFieldAttribute(100, ObjectFieldAlignment.Center));
        }

        if(member.Name == "prefab")
        {
            attributes.Add(new VerticalGroupAttribute("Character/Right"));
        }

        if(member.GetReturnType() == typeof(string))
        {
            attributes.Add(new VerticalGroupAttribute("Character/Right"));
        }

    }
}

public class StatsAttributeProcessor : OdinAttributeProcessor <Stats>
{
    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        attributes.Add(new BoxGroupAttribute("Stats"));
        attributes.Add(new RangeAttribute(0, 20));
    }

    public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
    {
        attributes.Add(new HideLabelAttribute());
        attributes.Add(new SpaceAttribute());
    }
}