using UnityEngine;

/// <summary>
/// Class responsible for bestowing powerup effects to the player when it 
/// collides with pickups.
/// </summary>
public class EffectHandler : MonoBehaviour
{
    [Tooltip("This entity's shield object")]
    public Shield Shield;

    [Tooltip("The speed at which the game is moving, and thus the apparent" +
        "speed of the entity.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Tooltip("Current number of bonuses collected by this entity.")]
    [SerializeField] private IntVariable bonusCount;

    [Header("Game Events")]

    [Tooltip("GameEvent to signal a bonus has been collected.")]
    [SerializeField] private GameEvent bonusCollected;

    [Tooltip("GameEvent to signal the game speed has decreased.")]
    [SerializeField] private GameEvent gameSpeedPickupDecreased;

    [Tooltip("GameEvent to signal the game speed has increased.")]
    [SerializeField] private GameEvent gameSpeedPickupIncreased;

    [Tooltip("GameEvent to signal a health point has been gained.")]
    [SerializeField] private GameEvent healed;

    /// <summary>
    /// Protects the entity from damage using a shield object.
    /// </summary>
    /// <param name="time">How long the entity is immune to damage.</param>
    public void ActivateShield(float time)
    {
        Shield.Activate(time);
    }

    /// <summary>
    /// Gives the player bonus points.
    /// </summary>
    /// <param name="count"></param>
    public void AddBonus(int count)
    {
        bonusCount.Value += count;
        bonusCollected.Raise();
    }

    /// <summary>
    /// Changes the speed at which objects move towards the player.
    /// </summary>
    /// <param name="amount">Amount by which to change the game speed.</param>
    public void ChangeGameSpeed(float amount)
    {
        if (gameSpeed.Value + amount > 0)
        {
            gameSpeed.Value += amount;
            if(amount > 0)
            {
                gameSpeedPickupIncreased.Raise();
            }
            else
            {
                gameSpeedPickupDecreased.Raise();
            }
        }
    }

    /// <summary>
    /// Raises the playerHealedEvent.
    /// </summary>
    /// <param name="count">Number of times to raise the event.</param>
    public void Heal(int count)
    {
        for (int i = 0; i < count; i++)
        {
            healed.Raise();
        }
    }
}
