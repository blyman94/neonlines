using UnityEngine;

/// <summary>
/// Allows the player to bypass the tutorial if they are feeling confident.
/// </summary>
public class SkipTutorial : MonoBehaviour
{
    [Tooltip("Game Event to signal that the tutorial is ended.")]
    [SerializeField] private GameEvent tutorialEnded;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialEnded.Raise();
        }
    }
}
