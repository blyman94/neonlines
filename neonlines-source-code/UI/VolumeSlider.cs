using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Represnts a slider that controls the volume of the game.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    [Tooltip("Audio Mixer to control volume of.")]
    [SerializeField] private AudioMixer mainMixer;

    [Tooltip("Key representing both the PlayerPrefs key that will be used" +
        "to save the volume, and the exposed parameter of the mixer that" +
        "will be controlled by the slider.")]
    [SerializeField] private string keyForVolumeSave;

    [Tooltip("Unity UI Slider used to control the volume.")]
    [SerializeField] private Slider slider;

    private void Start()
    {
        if(keyForVolumeSave == "musicVol")
        {
            slider.value = PlayerPrefs.GetFloat(keyForVolumeSave, 1.0f);
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat(keyForVolumeSave, 0.75f);
        }
    }

    /// <summary>
    /// Sets the volume of the main mixer to the new volume percentage,
    /// translated to the mixer's native logarithmic scale. Then, saves the 
    /// volume using PlayerPrefs to preserve volume changes between play
    /// sessions.
    /// </summary>
    /// <param name="newVolumePercent"></param>
    public void OnVolumeChange(float newVolumePercent)
    {
        mainMixer.SetFloat(keyForVolumeSave, 
            Mathf.Log(newVolumePercent) * 20);
        PlayerPrefs.SetFloat(keyForVolumeSave, newVolumePercent);
    }
}
