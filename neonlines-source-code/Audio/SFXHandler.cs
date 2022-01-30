using System.Collections;
using UnityEngine;

/// <summary>
/// Handles all sound effects in the game by listening for game events and 
/// responding with the playing of audio clips.
/// </summary>
public class SFXHandler : MonoBehaviour
{
    [Header("AudioSources")]

    [Tooltip("Source through which one-shot (non-looping) sounds will be " +
        "played.")]
    [SerializeField] private AudioSource sfxAudioSource;

    [Tooltip("Source through which looping sounds (shield) will be played.")]
    [SerializeField] private AudioSource sfxLoopingAudioSource;

    [Header("Bonus Audio")]

    [Tooltip("Audio clip for bonus.")]
    [SerializeField] private AudioClip bonusClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the bonus noise?")]
    [SerializeField] bool shiftBonusClipPitch = true;

    [Tooltip("How much should the pitch for bonus clip be shifted?")]
    [SerializeField, Range(0.01f,0.25f)] 
    private float bonusPitchShiftDelta = 0.05f;

    [Header("Contact Audio")]

    [Tooltip("Audio clip for contact.")]
    [SerializeField] private AudioClip contactClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the contact noise?")]
    [SerializeField] private bool shiftContactClipPitch = true;

    [Tooltip("How much should the pitch for contact clip be shifted?")]
    [SerializeField, Range(0.01f, 0.25f)] 
    private float contactPitchShiftDelta = 0.05f;

    [Header("Crash Audio")]

    [Tooltip("Audio clip for crash.")]
    [SerializeField] private AudioClip crashClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the crash noise?")]
    [SerializeField] private bool shiftCrashClipPitch = true;

    [Tooltip("How much should the pitch for crash clip be shifted?")]
    [SerializeField] private float crashPitchShiftDelta = 0.05f;

    [Header("Heal Audio")]

    [Tooltip("Audio clip for heal.")]
    [SerializeField] private AudioClip healClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the heal noise?")]
    [SerializeField] bool shiftHealClipPitch = true;

    [Tooltip("How much should the pitch for heal clip be shifted?")]
    [SerializeField] private float healPitchShiftDelta = 0.05f;

    [Header("Shield Audio")]

    [Tooltip("Audio clip for shield.")]
    [SerializeField] private AudioClip shieldActiveClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the shield noise?")]
    [SerializeField] bool shiftShieldActiveClipPitch = true;

    [Tooltip("How much should the pitch for shield clip be shifted?")]
    [SerializeField] private float shieldActivePitchShiftDelta = 0.05f;

    [Tooltip("How long should the shield take to power down?")]
    [SerializeField] private FloatVariable shieldDeactivateTime;

    [Header("Slow Down Audio")]

    [Tooltip("Audio clip for slow down.")]
    [SerializeField] private AudioClip slowDownClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the slow down noise?")]
    [SerializeField] bool shiftSlowDownClipPitch = true;

    [Tooltip("How much should the pitch for slow down clip be shifted?")]
    [SerializeField] private float slowDownPitchShiftDelta = 0.05f;

    [Header("Speed Up Audio")]

    [Tooltip("Audio clip for speed up.")]
    [SerializeField] private AudioClip speedUpClip;

    [Tooltip("Should the pitch be randomly shifted to create variance in" +
        "the speed up noise?")]
    [SerializeField] bool shiftSpeedUpClipPitch = true;

    [Tooltip("How much should the pitch for speed up clip be shifted?")]
    [SerializeField] private float speedUpPitchShiftDelta = 0.05f;

    /// <summary>
    /// Plays the clip representing bonus pickup.
    /// </summary>
    public void PlayBonusClip()
    {
        if (shiftBonusClipPitch)
        {
            sfxAudioSource.pitch = Random.Range(1 - bonusPitchShiftDelta, 
                1 + bonusPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(bonusClip, 1.0f);
    }

    /// <summary>
    /// Plays the clip representing contact with pickup.
    /// </summary>
    public void PlayContactClip()
    {
        if (shiftContactClipPitch)
        {
            sfxAudioSource.pitch = Random.Range(1 - contactPitchShiftDelta, 
                1 + contactPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(contactClip, 1.0f);
    }

    /// <summary>
    /// Plays the clip representing contact with obstacle.
    /// </summary>
    public void PlayCrashClip()
    {
        if (shiftCrashClipPitch)
        {
            sfxAudioSource.pitch = Random.Range(1 - crashPitchShiftDelta, 
                1 + crashPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(crashClip, 0.25f);
    }

    /// <summary>
    /// Plays the clip representing contact with health pickup.
    /// </summary>
    public void PlayHealClip()
    {
        if (shiftHealClipPitch)
        {
            sfxAudioSource.pitch =
                Random.Range(1 - healPitchShiftDelta, 1 + healPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(healClip, 1.0f);
    }

    /// <summary>
    /// Plays the clip representing an active shield.
    /// </summary>
    public void PlayShieldActiveClip()
    {
        if (shiftShieldActiveClipPitch)
        {
            sfxLoopingAudioSource.pitch = 
                Random.Range(1 - shieldActivePitchShiftDelta, 
                1 + shieldActivePitchShiftDelta);
        }
        else
        {
            sfxLoopingAudioSource.pitch = 1.0f;
        }

        sfxLoopingAudioSource.clip = shieldActiveClip;
        sfxLoopingAudioSource.volume = 1;
        if (!sfxLoopingAudioSource.isPlaying)
        {
            sfxLoopingAudioSource.Play();
        }
    }

    /// <summary>
    /// Plays the clip representing contact with slow down pickup.
    /// </summary>
    public void PlaySlowDownClip()
    {
        if (shiftSlowDownClipPitch)
        {
            sfxAudioSource.pitch = Random.Range(1 - slowDownPitchShiftDelta, 
                1 + slowDownPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(slowDownClip, 1.0f);
    }

    /// <summary>
    /// Plays the clip representing contact with speed up pickup.
    /// </summary>
    public void PlaySpeedUpClip()
    {
        if (shiftSpeedUpClipPitch)
        {
            sfxAudioSource.pitch = Random.Range(1 - speedUpPitchShiftDelta, 
                1 + speedUpPitchShiftDelta);
        }
        else
        {
            sfxAudioSource.pitch = 1.0f;
        }

        sfxAudioSource.PlayOneShot(speedUpClip, 1.0f);
    }

    /// <summary>
    /// Begins the routine to shut down the shield by slowly fading out its 
    /// volume.
    /// </summary>
    public void StopShieldActiveClip()
    {
        StopAllCoroutines();
        StartCoroutine(StopShieldRoutine());
    }

    /// <summary>
    /// Slowly fades out the shield active noise volume as the shield powerup
    /// ends.
    /// </summary>
    private IEnumerator StopShieldRoutine()
    {
        float elapsedTime = 0.0f;
        float startingVolume = sfxLoopingAudioSource.volume;
        while(elapsedTime < shieldDeactivateTime.Value)
        {
            sfxLoopingAudioSource.volume = Mathf.Lerp(startingVolume, 0.0f, 
                elapsedTime / shieldDeactivateTime.Value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sfxLoopingAudioSource.Stop();
    }
}
