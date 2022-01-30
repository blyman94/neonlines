using UnityEngine;

/// <summary>
/// Pickup effect that restores a set number of health points to the player.
/// </summary>
[CreateAssetMenu (fileName = "new Heal Effect", 
    menuName = "Pickup Effect.../Heal")]
public class HealEffect : PickupEffect
{
    [Tooltip("How many health points should be restored to the player?")]
    public int number;

    public override void Grant(EffectHandler effectHandler)
    {
        effectHandler.Heal(number);
    }
}
