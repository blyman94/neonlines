using UnityEngine;

/// <summary>
/// Pickup effect that prevents the player from taking all damage for a 
/// specified time.
/// </summary>
[CreateAssetMenu (fileName = "new Shield Effect", 
    menuName = "Pickup Effect.../Shield")]
public class ShieldEffect : PickupEffect
{
    [Tooltip("How long should the player be shielded from damage for?")]
    public float time;

    public override void Grant(EffectHandler effectHandler)
    {
        effectHandler.ActivateShield(time);
    }
}
