using TMPro;
using UnityEngine;

/// <summary>
/// Calculates a final score for the player and displays its breakdown in the
/// game over screen.
/// </summary>
public class FinalScoreDisplay : MonoBehaviour, IGameStateResponder
{
    [Tooltip("Game Event signaling a high score should be posted.")]
    [SerializeField] GameEvent postHighScore;

    [Header("Text UI")]

    [Tooltip("TextMeshPro Text object to display the player's raw score.")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Tooltip("TextMeshPro Text object to display the player's bonus count.")]
    [SerializeField] private TextMeshProUGUI bonusText;

    [Tooltip("TextMeshPro Text object to display the player's final score.")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Tooltip("TextMeshPro Text object telling the player if they have " +
        "placed in the Top 5 global high scores.")]
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    [Tooltip("TextMeshPro Input Field that allows players to enter their " +
        "initials upon receiving a new high score.")]
    [SerializeField] private TMP_InputField initialsInput;

    [Header("Variables")]

    [Tooltip("String representing the player's name.")]
    [SerializeField] private StringVariable playerName;

    [Tooltip("Int representing the player's raw score.")]
    [SerializeField] private IntVariable playerScore;

    [Tooltip("Int representing the player's bonus count.")]
    [SerializeField] private IntVariable playerBonus;

    [Tooltip("Int representing the player's final score.")]
    [SerializeField] private IntVariable playerFinalScore;

    [Tooltip("Int representing the minimum high score needed to make the " +
        "leaderboard.")]
    [SerializeField] private IntVariable minHighScore;

    [Tooltip("Int representing the bonus multiplier (how many points each " +
        "bonus pickup is worth.")]
    [SerializeField] private IntVariable bonusMultiplier;

    /// <summary>
    /// When true, this class will signal that a high score should be posted.
    /// </summary>
    private bool postScore = false;

    private void Start()
    {
        playerName.Value = "";
    }

    /// <summary>
    /// Changes the current name of the player to what is written in the input
    /// field.
    /// </summary>
    /// <param name="name">Name retrieved from input field.</param>
    public void UpdatePlayerName(string name)
    {
        playerName.Value = name;
    }

    public void OnGameReset()
    {
        if (postScore)
        {
            postHighScore.Raise();
            postScore = false;
        }
        playerName.Value = "";
    }

    public void OnGameRestart()
    {
        if (postScore)
        {
            postHighScore.Raise();
            postScore = false;
        }
        playerName.Value = "";
    }

    public void OnGameOver()
    {

    }

    public void OnGameStart()
    {

    }

    public void OnPlayerDied()
    {
        UpdateScoreText();
    }

    public void OnTutorialEnd()
    {

    }

    /// <summary>
    /// Updates the TextMeshPro Text objects to represent the player's score
    /// breakdown. Determines if the final score is worth of the leaderboard,
    /// and signals to post a score if so.
    /// </summary>
    private void UpdateScoreText()
    {
        newHighScoreText.gameObject.SetActive(false);
        initialsInput.gameObject.SetActive(false);
        postScore = false;

        scoreText.text = "Your Score: " + playerScore.Value;
        bonusText.text = "Bonus (x10): +" + playerBonus.Value * bonusMultiplier.Value;
        finalScoreText.text = "Final Score: " + playerFinalScore.Value;

        if(playerFinalScore.Value > minHighScore.Value)
        {
            newHighScoreText.gameObject.SetActive(true);
            initialsInput.gameObject.SetActive(true);
            postScore = true;
        }
    }
}
