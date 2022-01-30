using UnityEngine;

/// <summary>
/// GameObjects with the scroller component will move leftward at the
/// gameSpeed to give the illusion of player movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Scroller : MonoBehaviour
{
    [Tooltip("Float representing the current speed of the game.")]
    [SerializeField] private FloatVariable gameSpeed;

    /// <summary>
    /// Rigidbody2D component used to move the scroller leftward.
    /// </summary>
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateScrollSpeed();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deactivator"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Changes the scroll speed to the negative gameSpeed value if it is not
    /// at that speed currently.
    /// </summary>
    private void UpdateScrollSpeed()
    {
        if (rb2d.velocity.x != -gameSpeed.Value)
        {
            rb2d.velocity = new Vector2(-gameSpeed.Value, 0.0f);
        }
    }
}
