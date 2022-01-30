/// <summary>
/// Classes that implement this interface respond to game state events such as 
/// the game starting and ending.
/// </summary>
public interface IGameStateResponder
{
    /// <summary>
    /// Response to the game over event.
    /// </summary>
    void OnGameOver();

    /// <summary>
    /// Response to the game reset event.
    /// </summary>
    void OnGameReset();

    /// <summary>
    /// Response to the game start event.
    /// </summary>
    void OnGameStart();

    /// <summary>
    /// Response to the player died event.
    /// </summary>
    void OnPlayerDied();

    /// <summary>
    /// Response to the tutorial end event.
    /// </summary>
    void OnTutorialEnd();
}
