using System.Collections;
using UnityEngine;

/// <summary>
/// Represents the player. Responds to player input by flying upward. Collides
/// with obstacles and pickups. For this iteration, it is just a square!
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IGameStateResponder
{
    [Header("General")]

    [Tooltip("Reference to the effect handler class that gives the player" +
        "powerups when pickups are collected.")]
    [SerializeField] private EffectHandler effectHandler;

    [Tooltip("Force at which the player will fly upward.")]
    [SerializeField] private float upwardForce;

    [Tooltip("Stream from which the player will execute fly commands.")]
    [SerializeField] private FlyCommandStream commandStream;

    [Tooltip("Determines whether the player is currently in the tutorial. If" +
        "true, the player cannot take damage.")]
    [SerializeField] private BoolVariable tutorialActive;

    [Header("Variables")]

    [Tooltip("Float representing the player's current score.")]
    [SerializeField] private FloatVariable playerScoreFloat;

    [Tooltip("Int representing how many bonuses the player has collected.")]
    [SerializeField] private IntVariable playerBonusCount;

    [Header("Game Events")]

    [Tooltip("GameEvent to signal the player has lost a health point.")]
    [SerializeField] private GameEvent damaged;

    [Tooltip("GameEvent to signal the game has ended.")]
    [SerializeField] private GameEvent gameOver;

    [Header("Visuals")]

    [Tooltip("GameObject representing the player's graphics.")]
    [SerializeField] private GameObject playerGraphicsObject;

    [Tooltip("Particle system for the particles spawned on pickup.")]
    [SerializeField] private ParticleSystem playerParticleSystem;

    [Tooltip("Particle system renderer for the particles spawned on pickup.")]
    [SerializeField] private ParticleSystemRenderer playerParticleSystemRenderer;

    [Tooltip("Color of the player's outline.")]
    [SerializeField] private ColorVariable outlineColor;

    [Tooltip("Outline mesh renderer of the player.")]
    [SerializeField] private MeshRenderer outlineRenderer;

    /// <summary>
    /// Rigidbody2D component used for player movement.
    /// </summary>
    private Rigidbody2D rb2d;

    /// <summary>
    /// BoxCollider2D component used to detect player collision.
    /// </summary>
    private BoxCollider2D bc2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        OnGameReset();
        SetOutlineColor();
        SetParticleColor();
    }

    private void FixedUpdate()
    {
        GetCommandFromStream();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!effectHandler.Shield.IsActive && !tutorialActive.Value)
        {
            damaged.Raise();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (!effectHandler.Shield.IsActive && !tutorialActive.Value)
            {
                damaged.Raise();
            }
        }
        else if (collision.CompareTag("Pickup"))
        {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            if(pickup.Effect != null)
            {
                pickup.Effect.Grant(effectHandler);
            }
            pickup.OnPickUp();
        }
        else if (collision.CompareTag("Skip"))
        {
            playerScoreFloat.Value += 100;
            playerBonusCount.Value += 1;
        }
    }

    /// <summary>
    /// Adds upwardForce to the player's Rigidbody2D.
    /// </summary>
    public void Fly()
    {
        rb2d.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
    }

    public void OnGameOver()
    {
        
    }

    public void OnGameReset()
    {
        transform.position = Vector3.zero;
        bc2d.enabled = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        playerGraphicsObject.SetActive(true);
        effectHandler.Shield.IsActive = false;
    }

    public void OnGameStart()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    public void OnPlayerDied()
    {
        bc2d.enabled = false;
        playerGraphicsObject.SetActive(false);
        playerParticleSystem.Play();
        StopAllCoroutines();
        StartCoroutine(EndGameRoutine());
    }

    /// <summary>
    /// Activates the player's shield to ensure it cannot die during a game
    /// reset.
    /// </summary>
    public void OnResetButtonPressed()
    {
        effectHandler.Shield.IsActive = true;
    }

    public void OnTutorialEnd()
    {

    }

    private IEnumerator EndGameRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        gameOver.Raise();
    }

    /// <summary>
    /// Dequeues a command from the command stream and executes it if one is 
    /// available.
    /// </summary>
    private void GetCommandFromStream()
    {
        if (commandStream.Count() > 0)
        {
            commandStream.Dequeue().Execute(this);
        }
    }

    /// <summary>
    /// Changes the color of the player's outline to the value specified in
    /// outlineColor.
    /// </summary>
    private void SetOutlineColor()
    {
        if(outlineRenderer.material.GetColor("_BaseColor") != 
            outlineColor.Value)
        {
            outlineRenderer.material.SetColor("_BaseColor", outlineColor.Value);
        }

        if(outlineRenderer.material.GetColor("_EmissionColor") != 
            outlineColor.Value)
        {
            outlineRenderer.material.SetColor("_EmissionColor", outlineColor.Value);
        }
    }

    /// <summary>
    /// Changes the color of the player's particle effect to match its assigned
    /// color.
    /// </summary>
    private void SetParticleColor()
    {
        if (playerParticleSystemRenderer.material.GetColor("_BaseColor") !=
                    outlineColor.Value)
        {
            playerParticleSystemRenderer.material.SetColor("_BaseColor",
                outlineColor.Value);
        }

        if (playerParticleSystemRenderer.material.GetColor("_EmissionColor") !=
            outlineColor.Value)
        {
            playerParticleSystemRenderer.material.SetColor("_EmissionColor",
                outlineColor.Value);
        }
    }
}
