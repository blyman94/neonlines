using UnityEngine;

/// <summary>
/// Class representing collectable objects that change the game state during
/// runtime.
/// </summary>
public class Pickup : MonoBehaviour
{
    /// <summary>
    /// Property for the Pickup's assigned effect.
    /// </summary>
    public PickupEffect Effect
    {
        get
        {
            return effect;
        }
    }

    [Tooltip("Which effect does the player receive when colliding with this" +
        "pickup?")]
    [SerializeField] private PickupEffect effect;

    [Tooltip("Game event to signal a pickup has been collected.")]
    [SerializeField] private GameEvent collected;

    [Header("Appearance")]

    [Tooltip("Color variable determining the color of this pickup.")]
    [SerializeField] private ColorVariable color;

    [Tooltip("Sprite Renderer representing the pickup.")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Tooltip("Mesh Renderer representing the pickup's outline.")]
    [SerializeField] private MeshRenderer outlineMeshRenderer;

    [Header("Particle System")]

    [Tooltip("Particle system for the particles spawned on pickup.")]
    [SerializeField] private ParticleSystem pickupParticleSystem;

    [Tooltip("Particle system renderer for the particles spawned on pickup.")]
    [SerializeField] private ParticleSystemRenderer pickupParticleSystemRenderer;

    /// <summary>
    /// Determines if the pickup has been collected.
    /// </summary>
    private bool isPickedUp = false;

    private void OnEnable()
    {
        UpdateOutlineColor();
        UpdateParticleColor();
        UpdateSpriteColor();
    }

    private void OnDisable()
    {
        isPickedUp = false;
    }

    /// <summary>
    /// Hides the pickup and plays its particle effect.
    /// </summary>
    public void OnPickUp()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            collected.Raise();
            UpdateOutlineColor(Color.clear);
            UpdateSpriteColor(Color.clear);
            pickupParticleSystem.Play();
        }
    }

    /// <summary>
    /// Changes the color of the pickup's outline to match its assigned color.
    /// </summary>
    private void UpdateOutlineColor()
    {
        if (outlineMeshRenderer.material.GetColor("_BaseColor") !=
                    color.Value)
        {
            outlineMeshRenderer.material.SetColor("_BaseColor",
                color.Value);
        }

        if (outlineMeshRenderer.material.GetColor("_EmissionColor") !=
            color.Value)
        {
            outlineMeshRenderer.material.SetColor("_EmissionColor",
                color.Value);
        }
    }

    /// <summary>
    /// Changes the color of the pickup's outline to match a color passed to 
    /// the method.
    /// </summary>
    /// <param name="color">New color for the outline.</param>
    private void UpdateOutlineColor(Color color)
    {
        outlineMeshRenderer.material.SetColor("_BaseColor",color);
        outlineMeshRenderer.material.SetColor("_EmissionColor",color);
    }

    /// <summary>
    /// Changes the color of the pickup's particle effect to match its assigned
    /// color.
    /// </summary>
    private void UpdateParticleColor()
    {
        if (pickupParticleSystemRenderer.material.GetColor("_BaseColor") !=
                    color.Value)
        {
            pickupParticleSystemRenderer.material.SetColor("_BaseColor",
                color.Value);
        }

        if (pickupParticleSystemRenderer.material.GetColor("_EmissionColor") !=
            color.Value)
        {
            pickupParticleSystemRenderer.material.SetColor("_EmissionColor",
                color.Value);
        }
    }

    /// <summary>
    /// Changes the color of the pickup's sprite to match its assigned color.
    /// </summary>
    private void UpdateSpriteColor()
    {
        if (spriteRenderer.color != color.Value)
        {
            spriteRenderer.color = color.Value;
        }
    }

    /// <summary>
    /// Changes the color of the pickup's sprite to match a color passed to 
    /// the method.
    /// </summary>
    /// <param name="color">New color for the sprite.</param>
    private void UpdateSpriteColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
