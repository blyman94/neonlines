using UnityEngine;

/// <summary>
/// Class to spawn pickups in response to a spawnPickup event. Once the event
/// is recieved, this class will spawn a pickup after a random roll to see
/// whether a pickup should be spawned based on pickupSpawnChance. If 
/// a pickup is spawned, it is then offset in the x and y directions to add
/// variety to pickup placement.
/// </summary>
public class PickupSpawner : MonoBehaviour
{
    [Tooltip("Object pooler for the pickup object to be spawned.")]
    [SerializeField] private MultiObjectPooler pickupObjectPooler;

    [Tooltip("Chance that a pickup is spawned in between each pillar.")]
    [SerializeField] private FloatVariable pickupSpawnChance;

    [Tooltip("Range for pickup spawn heights.")]
    [SerializeField]
    private Vector2 pickupSpawnHeightRange;

    [Tooltip("Range for pickup spawn x offsets.")]
    [SerializeField]
    private Vector2 pickupSpawnXOffsetRange;

    private void Start()
    {
        pickupObjectPooler.InitializePool();
    }

    /// <summary>
    /// Deactivates all pooled objects when the game is reset.
    /// </summary>
    public void OnGameReset()
    {
        pickupObjectPooler.DeactivateAll();
    }

    /// <summary>
    /// Determines if a pickup should be spawned based on pickupSpawnChance.
    /// </summary>
    public void OnSpawnPickup()
    {
        if(Random.value <= pickupSpawnChance.Value)
        {
            SpawnPickup();
        }
    }

    /// <summary>
    /// Spawns a pickup object from the object pool at a random height.
    /// </summary>
    private void SpawnPickup()
    {
        GameObject pickupObject = pickupObjectPooler.GetRandomObject();

        if(pickupObject != null)
        {
            float randomSpawnHeight = Random.Range(pickupSpawnHeightRange.x,
                pickupSpawnHeightRange.y);
            float randomXOffset = Random.Range(pickupSpawnXOffsetRange.x,
                pickupSpawnXOffsetRange.y);

            pickupObject.transform.position = 
                new Vector3(transform.position.x + randomXOffset,
                randomSpawnHeight, transform.position.z);

            pickupObject.SetActive(true);
        }
    }
}
