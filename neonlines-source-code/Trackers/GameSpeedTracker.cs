using UnityEngine;

/// <summary>
/// Responsible for increasing the game speed ambiently as the game progresses.
/// </summary>
public class GameSpeedTracker : MonoBehaviour, IGameStateResponder
{
    [Tooltip("Float representing the game speed.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Tooltip("Starting speed for the game.")]
    [SerializeField] private float startingGameSpeed = 4.0f;

    [Tooltip("The game speed will increase by this much each interval.")]
    [SerializeField] private float speedDelta;

    [Tooltip("Game will speed up every ____ seconds.")]
    [SerializeField] private float speedUpInterval;

    /// <summary>
    /// When true, the game will speed up by speedDelta event speedUpInterval
    /// seconds.
    /// </summary>
    private bool accelerating = false;

    private void Awake()
    {
        gameSpeed.Value = 0.0f;
    }

    private void Update()
    {
        if (accelerating)
        {
            IncreaseGameSpeed();
        }
    }

    public void OnGameOver()
    {
        
    }

    public void OnGameReset()
    {
        gameSpeed.Value = 0.0f;
    }

    public void OnGameStart()
    {
        gameSpeed.Value = startingGameSpeed;
    }

    public void OnPlayerDied()
    {
        accelerating = false;
        gameSpeed.Value = 0.0f;
    }

    public void OnTutorialEnd()
    {
        accelerating = true;
    }

    /// <summary>
    /// Increases the game speed by speedDelta every speedUpInterval seconds.
    /// </summary>
    private void IncreaseGameSpeed()
    {
        gameSpeed.Value += (speedDelta / speedUpInterval) * Time.deltaTime;
    }
}
