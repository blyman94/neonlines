using UnityEngine;

/// <summary>
/// Handles the health display, responding when the entity is damaged by 
/// deactivating the right most health image. Invokes an event when the entity
/// runs out of health.
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [Tooltip("Array of health images used to track player health in UI.")]
    [SerializeField] private GameObject[] healthImages;

    [Tooltip("Event to raise when the player runs out of health.")]
    [SerializeField] private GameEvent playerDied;

    /// <summary>
    /// Reduces the player's health by 1 in response to a damaged event. Will
    /// signal playerDied when health reaches 0.
    /// </summary>
    public void OnDamaged()
    {
        for(int i = healthImages.Length - 1; i >= 1; i--)
        {
            if (healthImages[i].activeInHierarchy)
            {
                healthImages[i].SetActive(false);
                return;
            }
        }

        healthImages[0].SetActive(false);
        playerDied.Raise();
    }

    /// <summary>
    /// Activates all health image objects.
    /// </summary>
    public void OnGameReset()
    {
        foreach (GameObject healthImage in healthImages)
        {
            healthImage.SetActive(true);
        }
    }

    /// <summary>
    /// If the player is missing any health, it will activate the rightmost
    /// missing health image to represent the player healing.
    /// </summary>
    public void OnHealed()
    {
        foreach (GameObject healthImage in healthImages)
        {
            if (!healthImage.activeInHierarchy)
            {
                healthImage.SetActive(true);
                return;
            }
        }
    }
}
