#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidationRule(typeof(PlayerUnitValidator), Name = "PlayerUnitValidator")]

public class PlayerUnitValidator : RootObjectValidator<HexGame.Units.PlayerUnit>
{
    protected override void Validate(ValidationResult result)
    {
        if (this.Object.gameObject.layer != 7 && this.Object.gameObject.layer != 9)
            result.AddError("Player Unit object is not on the player object layer.")
                  .WithFix("Set Correct Layer =>", () => SetLayerToPlayerUnit(this.Object.gameObject), true);

        if (this.Object.GetComponent<Collider>() == null)
            result.AddError("Player Unit does not have a collider.");

        if (this.Object.GetStat(Stat.sightDistance) <= 0.01f)
            result.AddError("Sight distance not set.")
                  .WithFix(() => this.Object.GetStats().AddStat(Stat.sightDistance, 3f), true);
    }

    private void SetLayerToPlayerUnit(GameObject gameObject)
    {
        gameObject.layer = 7;
    }
}
#endif


