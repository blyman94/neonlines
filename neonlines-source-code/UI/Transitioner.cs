using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for visually fading between scenes.
/// </summary>
public class Transitioner : MonoBehaviour
{
    [Tooltip("Image to be used to fade between screens. Should cover the" +
        "entire viewport of the game.")]
    [SerializeField] private Image transitionFader;

    [Tooltip("CanvasGroup that contains the transition fader.")]
    [SerializeField] private CanvasGroup transitionGroup;

    [Tooltip("A references to the game's GameLoopHandler.")]
    [SerializeField] private GameLoopHandler gameLoopHandler;

    [Tooltip("Game Event to be raised when transitioning to the game screen.")]
    [SerializeField] private GameEvent toGame;

    [Tooltip("Game Event to be raised when transitioning to the title screen.")]
    [SerializeField] private GameEvent gameReset;

    [Tooltip("How long the transition should take. The first half of " +
        "this time will be spent fading out the current screen, and the " +
        "second half of this time will be spent fading in the destination " +
        "screen.")]
    [SerializeField] private FloatVariable transitionTime;

    private IEnumerator Start()
    {
        yield return FadeInRoutine();
    }

    /// <summary>
    /// Fades out of the game screen, signals for a game reset, then fades
    /// back in the game screen.
    /// </summary>
    public void ResetGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(gameReset));
    }

    /// <summary>
    /// Transitions to the game screen.
    /// </summary>
    public void ToGame()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(toGame));
    }

    /// <summary>
    /// Fades out the game screen in preparation for a game restart.
    /// </summary>
    public void OnGameRestart()
    {
        StartCoroutine(RestartGameRoutine());
    }

    /// <summary>
    /// Fades out the game screen in prepartaion for a game restart.
    /// </summary>
    private IEnumerator RestartGameRoutine()
    {
        yield return FadeOutRoutine();
        gameLoopHandler.Restart();
    }

    /// <summary>
    /// Uses the transitionFader to fade out the current screen.
    /// </summary>
    private IEnumerator FadeOutRoutine()
    {
        transitionGroup.blocksRaycasts = true;
        float elapsedTime = 0.0f;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            transitionFader.color =
                Color.Lerp(Color.clear, Color.white, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.white;
    }

    /// <summary>
    /// Uses the transitionFader to fade in the next screen.
    /// </summary>
    private IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0.0f;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            transitionFader.color =
                Color.Lerp(Color.white, Color.clear, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transitionFader.color = Color.clear;
        transitionGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Raises the passed transitionEvent between FadeOutRoutine and 
    /// FadeInRoutine.
    /// </summary>
    private IEnumerator TransitionRoutine(GameEvent transitionEvent)
    {
        yield return FadeOutRoutine();
        transitionEvent.Raise();
        yield return FadeInRoutine();
    }
}
