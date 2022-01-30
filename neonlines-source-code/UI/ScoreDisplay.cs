using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for displaying the entity's score and bonus count during 
/// gameplay.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("TextMeshPro Text object used to display in-game current score.")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Tooltip("TextMeshPro Text object used to display in-game current bonus " +
        "count.")]
    [SerializeField] private TextMeshProUGUI bonusText;

    [Tooltip("Color of the bonus text.")]
    [SerializeField] private ColorVariable bonusColor;

    [Tooltip("Int representing the current player's score.")]
    [SerializeField] private IntVariable score;

    [Tooltip("Int representing the current player's bonus count.")]
    [SerializeField] private IntVariable bonus;

    private void Start()
    {
        OnBonusCollected();
        UpdateScoreText();
        UpdateBonusTextColor();
    }

    private void LateUpdate()
    {
        UpdateScoreText();
    }

    /// <summary>
    /// Changes the bonus text when a bonus is collected.
    /// </summary>
    public void OnBonusCollected()
    {
        bonusText.text = "Bonus: " + bonus.Value;
    }

    /// <summary>
    /// Resets the score and bonus values when the game starts.
    /// </summary>
    public void OnGameStart()
    {
        OnBonusCollected();
        UpdateScoreText();
    }

    /// <summary>
    /// Changes the text color of the bonus text to match its assigned color.
    /// </summary>
    private void UpdateBonusTextColor()
    {
        if (bonusText.color != bonusColor.Value)
        {
            bonusText.color = bonusColor.Value;
        }
    }

    /// <summary>
    /// Updates the score text to reflect the player's current score and bonus
    /// count.
    /// </summary>
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.Value;
        bonusText.text = "Bonus: " + bonus.Value;
    }
}
