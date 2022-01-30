using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Accepts use input from the New Unity Input System. Creates commands based
/// on the user input, and enqueues them in a command stream for consumption 
/// by the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Stream from which the player will execute fly commands.")]
    [SerializeField] private FlyCommandStream commandStream;

    /// <summary>
    /// Command to pass to the player to have it fly upward.
    /// </summary>
    private FlyCommand flyCommand;

    private void Start()
    {
        flyCommand = new FlyCommand();
    }

    /// <summary>
    /// Response to "Fly" input from the New Unity Input System. Enqueues a 
    /// fly command to the command stream.
    /// </summary>
    /// <param name="context">Input action context.</param>
    public void OnFly(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            commandStream.Enqueue(flyCommand);
        }
    }
}
