using UnityEngine;
using System;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(NeedsComponentValidator))]

public class NeedsComponentAttribute : Attribute
{
    public Type type;

    public NeedsComponentAttribute(Type type)
    {
        this.type = type;
    }
}

public class NeedsComponentValidator : AttributeValidator<NeedsComponentAttribute, GameObject>
{
    protected override void Validate(ValidationResult result)
    {
        if (this.ValueEntry.SmartValue == null)
            return;

        if (this.ValueEntry.SmartValue.GetComponent(this.Attribute.type) == null)
        {
            result.ResultType = ValidationResultType.Error;
            result.Message = "This Needs a " + this.Attribute.type.Name;
        }
    }
}



