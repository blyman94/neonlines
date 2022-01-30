using UnityEngine;

/// <summary>
/// Abstract class of effects that can be gained when the player collects 
/// pickups.
/// </summary>
public abstract class PickupEffect : ScriptableObject
{
    /// <summary>
    /// Bestows the pickup effect onto the player.
    /// </summary>
    /// <param name="effectHandler">Handler object for the given 
    /// effect.</param>
    public abstract void Grant(EffectHandler effectHandler);
}
