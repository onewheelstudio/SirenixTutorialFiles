using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

public class SamplePropertyProcessor<T> : OdinPropertyProcessor<T> where T : SampleClass
{
    public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
    {
        for (int i = 0; i < propertyInfos.Count; i++)
        {
            if(propertyInfos[i].PropertyName == "bottom")
            {
                propertyInfos.Insert(0, propertyInfos[i]);
                propertyInfos.RemoveAt(i + 1);
            }
        }

        propertyInfos.AddDelegate("Print Hello", () => Debug.Log("Hello"), new BoxGroupAttribute("injected"));

        propertyInfos.AddValue("Injected Property",
            (ref SampleClass s) => s.value1 + s.value2 + s.value3,
            (ref SampleClass s, int sum) => { }, new BoxGroupAttribute("injected"));

        propertyInfos.AddValue("Injected Enum",
            (ref SampleClass s) => (MyEnum)s.value1,
            (ref SampleClass s, MyEnum myEnum) => s.value1 = (int)myEnum,
            new EnumToggleButtonsAttribute(),
            new BoxGroupAttribute("injected"));

        propertyInfos.Remove("value1");
    }
}
