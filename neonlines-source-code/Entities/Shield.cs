using System.Collections;
using UnityEngine;

/// <summary>
/// Represents the shield object. When the shield is active, the shielded
/// entity cannot take damage.
/// </summary>
public class Shield : MonoBehaviour
{
    /// <summary>
    /// Determines if the player can take damage and die.
    /// </summary>
    public bool IsActive { get; set; } = false;

    [Tooltip("How long the shield takes to fully shut down after receiving" +
        "a signal to do so.")]
    [SerializeField] private FloatVariable deactivationTime;

    [Tooltip("Blinking signals that the shield is about to deactivate. How" +
        "many times should it blink while shutting down?")]
    [SerializeField] private int numBlinks;

    [Header("Appearance")]

    [Tooltip("Sprite renderer representing the shield's graphics.")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Tooltip("Color of the shield")]
    [SerializeField] private ColorVariable shieldColor;

    [Header("Game Events")]

    [Tooltip("GameEvent to signal a shield has been activated.")]
    [SerializeField] private GameEvent shieldActivated;

    [Tooltip("GameEvent to signal a shield has been deactivated.")]
    [SerializeField] private GameEvent shieldDeactivated;

    private void Start()
    {
        spriteRenderer.color = Color.clear;
    }

    /// <summary>
    /// Makes the player invulnerable. Then, starts a coroutine to end the 
    /// invulnerability for after the passed time.
    /// </summary>
    /// <param name="time">Amount of time the effect will last.</param>
    public void Activate(float time)
    {
        IsActive = true;
        shieldActivated.Raise();
        ShowGraphics();

        StopAllCoroutines();
        StartCoroutine(DeactivateRoutine(time));
    }

    /// <summary>
    /// Deactivates the shield instantly when the game resets.
    /// </summary>
    public void OnGameReset()
    {
        StopAllCoroutines();
        IsActive = false;
        spriteRenderer.color = Color.clear;
    }

    /// <summary>
    /// Routine to make the player vulnerable after a specified amount of time.
    /// </summary>
    /// <param name="time">Time after which the player becomes 
    /// vulnerable.</param>
    private IEnumerator DeactivateRoutine(float time)
    {
        yield return new WaitForSeconds(time - deactivationTime.Value);
        shieldDeactivated.Raise();
        yield return BlinkRoutine();
        IsActive = false;
    }

    /// <summary>
    /// Blinks the shield graphics so the player knows the shield is about
    /// to wear off.
    /// </summary>
    private IEnumerator BlinkRoutine()
    {
        float eachBlinkDuration = deactivationTime.Value / numBlinks;
        for(int i = 0; i < numBlinks; i++)
        {
            float elapsedTime = 0.0f;
            spriteRenderer.color = shieldColor.Value;
            while(elapsedTime < eachBlinkDuration)
            {
                spriteRenderer.color = 
                    Color.Lerp(shieldColor.Value, Color.clear, 
                    elapsedTime / eachBlinkDuration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            spriteRenderer.color = Color.clear;
        }
    }

    /// <summary>
    /// Shows the shield graphics.
    /// </summary>
    private void ShowGraphics()
    {
        Color color = new Color(shieldColor.Value.r, shieldColor.Value.g, 
            shieldColor.Value.b, 1.0f);
        spriteRenderer.color = color;
    }
}
