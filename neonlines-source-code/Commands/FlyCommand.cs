/// <summary>
/// A command used to tell the player to float upward when executed.
/// </summary>
public class FlyCommand
{
    /// <summary>
    /// Changes the player input to match input from the player controller.
    /// </summary>
    /// <param name="player">Player to execute this command.</param>
    public void Execute(Player player)
    {
        player.Fly();
    }
}
