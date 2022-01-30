using Cinemachine;
using System.Collections;
using UnityEngine;

/// <summary>
/// Utilizes Cinemachine Basic Multi Channel Perlin noise to shake the camera
/// when an obstacle is hit or when a pickup is collected.
/// </summary>
public class CinemachineShake : MonoBehaviour
{
    [Tooltip("The camera to be shaken on gameplay events.")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Shake Parameters")]

    [Tooltip("Duration of the shake when the player collides with an " +
        "obstacle.")]
    [SerializeField] private float playerDamagedShakeDuration;

    [Tooltip("Intensity of the shake when the player collides with an " +
        "obstacle. Should be more intense than for pickups.")]
    [SerializeField] private float playerDamagedShakeIntensity;

    [Tooltip("Duration of the shake when the player collides with a " +
        "pickup.")]
    [SerializeField] private float playerPickupShakeDuration;

    [Tooltip("Intensity of the shake when the player collides with a " +
        "pickup. Should be less intense than for obstacles.")]
    [SerializeField] private float playerPickupShakeIntensity;

    /// <summary>
    /// The Basic Multi Channel Perlin noise component to be controlled by the
    /// shake methods.
    /// </summary>
    private CinemachineBasicMultiChannelPerlin cameraNoiseComponent;

    private void Awake()
    {
        cameraNoiseComponent = 
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    /// <summary>
    /// Shakes the camera with playerDamageShakeDuration and 
    /// playerDamagedShakeIntensity.
    /// </summary>
    public void PlayerDamagedShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(playerDamagedShakeDuration,
            playerDamagedShakeIntensity));
    }

    /// <summary>
    /// Shakes the camera with playerPickupShakeDuration and 
    /// playerPickupShakeIntensity.
    /// </summary>
    public void PlayerPickupShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(playerPickupShakeDuration,
            playerPickupShakeIntensity));
    }

    /// <summary>
    /// Shakes the camera for the passed duration, starting at the passed
    /// intensity. Gently fades the noise out over the duration.
    /// </summary>
    /// <param name="shakeDuration">How long to shake the camera for.</param>
    /// <param name="shakeIntensity">The starting point of how intense
    /// the shake should be.</param>
    private IEnumerator ShakeRoutine(float shakeDuration, float shakeIntensity)
    {
        float elapsedTime = 0;
        cameraNoiseComponent.m_AmplitudeGain = shakeIntensity;

        while (elapsedTime < shakeDuration)
        {
            cameraNoiseComponent.m_AmplitudeGain = 
                Mathf.Lerp(shakeIntensity, 0.0f, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraNoiseComponent.m_AmplitudeGain = 0.0f;
    }

}
