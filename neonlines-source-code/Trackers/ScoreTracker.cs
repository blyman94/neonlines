using UnityEngine;

/// <summary>
/// Responsible for updating the score and bonus count based on gameplay.
/// </summary>
public class ScoreTracker : MonoBehaviour, IGameStateResponder
{
    [Tooltip("Score variable to update.")]
    [SerializeField] private IntVariable score;

    [Tooltip("Bonus variable to update")]
    [SerializeField] private IntVariable bonus;

    [Tooltip("How many points is each bonus worth?")]
    [SerializeField] private IntVariable bonusMultiplier;

    [Tooltip("Final score")]
    [SerializeField] private IntVariable finalScore;

    [Tooltip("Current game speed.")]
    [SerializeField] private FloatVariable gameSpeed;

    [Tooltip("Float representing the current score of the player. This is" +
        "an intermediate variable in use because the score is displayed as" +
        "an integer but calculated as a float, because the distance the " +
        "player travels is continuous.")]
    [SerializeField] private FloatVariable scoreFloat;

    /// <summary>
    /// When true, score ticks up based on distance traveled.
    /// </summary>
    private bool scoring = false;

    private void Awake()
    {
        scoreFloat.Value = 0;
        score.Value = 0;
        bonus.Value = 0;
    }

    private void Update()
    {
        if (scoring)
        {
            UpdateScore();
        }
    }

    public void OnGameOver()
    {
        
    }

    public void OnGameReset()
    {
        scoreFloat.Value = 0;
        score.Value = 0;
        bonus.Value = 0;
    }

    public void OnGameStart()
    {
        scoring = true;
    }

    public void OnPlayerDied()
    {
        scoring = false;
    }

    public void OnTutorialEnd()
    {

    }

    /// <summary>
    /// Increases the score based on game speed then converts the results to
    /// an integer for display.
    /// </summary>
    private void UpdateScore()
    {
        scoreFloat.Value += gameSpeed.Value * Time.deltaTime;
        score.Value = Mathf.RoundToInt(scoreFloat.Value);
        finalScore.Value = score.Value + (bonus.Value * bonusMultiplier.Value);
    }
}
