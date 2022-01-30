using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the random and endless spawning of pillars for the player to 
/// fly through.
/// </summary>
public class PillarSpawner : MonoBehaviour, IGameStateResponder
{
    [Header("General")]

    [Tooltip("Object pooler for pillar objects. It is recommended that this" +
        "pooler is able to grow, to accomodate for game speed changes.")]
    [SerializeField] private ObjectPooler pillarObjectPooler;

    [Tooltip("A random spawn time is chosen from this range each time a" +
        "pillar is spawned.")]
    [SerializeField] private Vector2Variable pillarSpawnTimeRange;

    [Tooltip("Current speed of the game.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Tooltip("Event to signal to pickup spawner that a pickup may be " +
        "spawned.")]
    [SerializeField] private GameEvent spawnPickup;

    [Header("Pillar Spawn Parameters")]

    [Tooltip("Height at which the ground starts. Ensures pillars do not " +
        "overlap with the ground.")]
    [SerializeField] private float groundStart;

    [Tooltip("Height of the ceiling. Ensures pillars do not overlap with " +
        "the ceiling.")]
    [SerializeField] private float ceilingHeight;

    [Tooltip("Range for lower pillar heights.")]
    [SerializeField] 
    private Vector2 lowerPillarHeightRange;

    [Tooltip("Range for pillar widths.")]
    [SerializeField]
    private Vector2 pillarWidthRange;

    [Tooltip("Range for gap heights. The low value should be greater than 1 " +
        "so that the player can fit through any gap generated. ")]
    [SerializeField] 
    private Vector2 gapHeightRange;

    private void Start()
    {
        pillarObjectPooler.InitializePool();
    }

    public void OnGameStart()
    {

    }

    public void OnGameReset()
    {
        StopAllCoroutines();
        pillarObjectPooler.DeactivateAll();
    }

    public void OnGameOver()
    {
        
    }

    public void OnPlayerDied()
    {
        StopAllCoroutines();
    }

    public void OnTutorialEnd()
    {
        StartCoroutine(SpawnAtIntervals());
    }

    /// <summary>
    /// Changes how often the pillars spawn to keep their distance range
    /// proportional to the current game speed.
    /// </summary>
    public void UpdateSpawnTiming()
    {
        pillarSpawnTimeRange.Value.x = 12 / gameSpeed.Value;
        pillarSpawnTimeRange.Value.y = 16 / gameSpeed.Value;
    }

    /// <summary>
    /// Routine to spawn pillar obstacles after an initial spawn delay and at 
    /// random times between the low and high values of pillarSpawnTimeRange.
    /// </summary>
    private IEnumerator SpawnAtIntervals()
    {
        while (true)
        {
            UpdateSpawnTiming();
            float pillarSpawnTime = Random.Range(pillarSpawnTimeRange.Value.x,
                pillarSpawnTimeRange.Value.y);
            yield return new WaitForSeconds(pillarSpawnTime * 0.5f);
            spawnPickup.Raise();
            yield return new WaitForSeconds(pillarSpawnTime * 0.5f);
            SpawnPillar();
        }
    }

    /// <summary>
    /// Determines a random height for the lower column of each pillar and a 
    /// random gap height, then deduces an upper pillar height to create a 
    /// complete pillar obstacle. 
    /// </summary>
    private void SpawnPillar()
    {
        float lowerPillarHeight = Random.Range(lowerPillarHeightRange.x,
            lowerPillarHeightRange.y);
        float gapHeight = Random.Range(gapHeightRange.x, gapHeightRange.y);
        float upperPillarHeight =
            ceilingHeight - lowerPillarHeight - gapHeight;
        float randomWidth = Random.Range(pillarWidthRange.x,
            pillarWidthRange.y);

        // Lower Pillar
        GameObject lowerPillarObject = pillarObjectPooler.GetObject();
        lowerPillarObject.SetActive(true);
        lowerPillarObject.transform.position = 
            new Vector3(transform.position.x, groundStart + 
            (lowerPillarHeight * 0.5f), transform.position.z);

        Pillar lowerPillar = lowerPillarObject.GetComponent<Pillar>();
        lowerPillar.SetHeight(lowerPillarHeight);
        lowerPillar.SetWidth(randomWidth);

        // Upper Pillar
        GameObject upperPillarObject = pillarObjectPooler.GetObject();
        upperPillarObject.SetActive(true);
        upperPillarObject.transform.position = 
            new Vector3(transform.position.x, groundStart + 
            lowerPillarHeight + gapHeight + (upperPillarHeight * 0.5f),
            transform.position.z);

        Pillar upperPillar = upperPillarObject.GetComponent<Pillar>();
        upperPillar.SetHeight(upperPillarHeight);
        upperPillar.SetWidth(randomWidth);
    }
}
