using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utilizes Unity's Scene Management package to allow for restarting and 
/// quitting of the application. Also allows for pausing.
/// </summary>
[CreateAssetMenu]
public class GameLoopHandler : ScriptableObject
{
    [Tooltip("Game event to signal the game has been paused/unpaused.")]
    [SerializeField] private GameEvent gamePaused;

    /// <summary>
    /// Determines whether the game is currently paused.
    /// </summary>
    private bool paused = false;

    private void OnEnable()
    {
        paused = false;
    }

    /// <summary>
    /// Toggles pause behavior by switching Time.timeScale between 1 and 0.
    /// Also raises an event to signal the game's pause state has been
    /// switched.
    /// </summary>
    public void Pause()
    {
        gamePaused.Raise();
        if (paused)
        {
            paused = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            paused = true;
            Time.timeScale = 0.0f;
        }
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarts the application by reloading the scene.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
