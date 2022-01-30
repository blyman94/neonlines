using System.Collections;
using UnityEngine;

/// <summary>
/// Ensures that each soundtrack is played in the appropriate scene.
/// </summary>
public class MusicHandler : MonoBehaviour
{
    [Tooltip("Audio source through which music will be played. Should loop, " +
        "and should play on awake.")]
    [SerializeField] private AudioSource musicAudio;

    [Tooltip("The length of a transition between two scenes. The music will" +
        "slowly fade out during the first half of the transition, then " +
        "slowly fade in during the second half of the transition.")]
    [SerializeField] private FloatVariable transitionTime;

    [Tooltip("Audio Clip that contains the title screen music.")]
    [SerializeField] private AudioClip titleScreenMusic;

    [Tooltip("Audio Clip that contains the gameplay music.")]
    [SerializeField] private AudioClip gameScreenMusic;

    /// <summary>
    /// Transitions the music handler to the game screen.
    /// </summary>
    public void ToGame()
    {
        StopAllCoroutines();
        StartCoroutine(MusicTransitionRoutine(gameScreenMusic));
    }

    /// <summary>
    /// Slowly fades the music out during the first half of transitionTime,
    /// then slowly fades the music in during the second half of transitionTime.
    /// </summary>
    /// <param name="transitionToClip">Destination clip of the transition.
    /// </param>
    private IEnumerator MusicTransitionRoutine(AudioClip transitionToClip)
    {
        float elapsedTime = 0.0f;
        float startVolume = musicAudio.volume;
        float halfTime = transitionTime.Value * 0.5f;
        while (elapsedTime < halfTime)
        {
            musicAudio.volume = Mathf.Lerp(startVolume, 0.0f, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        musicAudio.volume = 0.0f;

        musicAudio.clip = transitionToClip;
        musicAudio.Play();

        elapsedTime = 0.0f;
        while (elapsedTime < halfTime)
        {
            musicAudio.volume = Mathf.Lerp(0.0f, 1.0f, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        musicAudio.volume = 1.0f;
    }
}
