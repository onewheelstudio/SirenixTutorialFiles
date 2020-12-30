using UnityEngine;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.OdinInspector.Editor;

[assembly: RegisterValidator(typeof(PlantValidator))]
public class PlantValidator : ValueValidator<Plant>
{
    public override bool CanValidateProperty(InspectorProperty property)
    {
        return property.IsTreeRoot;
    }

    protected override void Validate(ValidationResult result)
    {
        if (this.ValueEntry.SmartValue == null)
            return;

        if(this.ValueEntry.SmartValue.transform.childCount == 0)
        {
            result.ResultType = ValidationResultType.Error;
            result.Message = "This Plant Doesn't Have a Child Object";
        }
        else
        {
            Transform child = this.ValueEntry.SmartValue.transform.GetChild(0);

            if (child.GetComponent<Collider>() == null)
            {
                result.ResultType = ValidationResultType.Warning;
                result.Message = "The plant mesh is missing a collider";
            }
            else if (child.GetComponent<Rigidbody>() == null)
            {
                result.ResultType = ValidationResultType.Warning;
                result.Message = "The plant mesh is missing a rigidbody";
            }
            else if(!child.GetComponent<Rigidbody>().isKinematic)
            {
                result.ResultType = ValidationResultType.Warning;
                result.Message = "The plant rigidbody is not set to isKinematic";
            }
        }
    }
}




