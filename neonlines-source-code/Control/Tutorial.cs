using UnityEngine;

/// <summary>
/// Tracks the state of the tutorial. Can reset tutorial objects to their 
/// original positions, and deactivate all of them when the tutorial ends.
/// </summary>
public class Tutorial : MonoBehaviour
{
    [Tooltip("Array of tutorial objects to be controlled by the Tutorial " +
        "class.")]
    [SerializeField] private GameObject[] tutorialObjects;

    [Tooltip("Array of Vector3 objects representing the original positions " +
        "of all tutorial objects.")]
    [SerializeField] private Vector3[] tutorialObjectStartPositions;

    [Tooltip("Event to signal the tutorial has ended and actual gameplay" +
        "rules take hold.")]
    [SerializeField] private GameEvent gameStarted;

    [Tooltip("Determines whether the tutorial is currently active. Some " +
        "game actors may behave differently during the tutorial than in" +
        "actual gameplay.")]
    [SerializeField] private BoolVariable tutorialActive;

    private void Start()
    {
        tutorialActive.Value = true;
        InitializeTutorialObjectPositions();
    }

    /// <summary>
    /// Caches the initial positions of all tutorial objects for the purpose 
    /// of resetting later.
    /// </summary>
    private void InitializeTutorialObjectPositions()
    {
        tutorialObjectStartPositions = new Vector3[tutorialObjects.Length];
        for (int i = 0; i < tutorialObjectStartPositions.Length; i++)
        {
            tutorialObjectStartPositions[i] =
                tutorialObjects[i].transform.position;
        }
    }

    /// <summary>
    /// Ends the tutorial.
    /// </summary>
    public void EndTutorial()
    {
        tutorialActive.Value = false;
        DeactivateTutorialObjects();
        gameStarted.Raise();
    }

    /// <summary>
    /// Deactivates all tutorial objects.
    /// </summary>
    public void DeactivateTutorialObjects()
    {
        foreach (GameObject tutorialObject in tutorialObjects)
        {
            tutorialObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the tutorial to the active state, resets the position of all
    /// tutorial objects, and activates all tutorial objects in response to 
    /// the game resetting.
    /// </summary>
    public void OnGameReset()
    {
        tutorialActive.Value = true;
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(true);
            tutorialObjects[i].transform.position = 
                tutorialObjectStartPositions[i];
        }
    }
}
