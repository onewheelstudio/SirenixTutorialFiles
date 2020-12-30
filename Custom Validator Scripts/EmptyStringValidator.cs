using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(EmptyStringValidator))]

public class EmptyStringValidator : ValueValidator<string>
{
    protected override void Validate(ValidationResult result)
    {
        if (string.IsNullOrEmpty(this.ValueEntry.SmartValue))
        {
            result.ResultType = ValidationResultType.Warning;
            result.Message = "This string is empty! Are you sure that's correct?";
        }
    }
}
