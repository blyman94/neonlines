using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listener object for the ScriptableObject-based implementation of an event 
/// system. When enabled, it registers itself to the assigned event. When 
/// disabled, it unregisters itself from the assigned event. When the assigned
/// event is raised, it will respond with a UnityEvent that can be configured
/// in the inspector.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Subject event of this listener.")]
    public GameEvent Event;

    [Tooltip("Response when subject event is raised.")]
    public UnityEvent Response;

    public void OnEnable()
    {
        Event.RegisterListener(this);
    }

    public void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    /// <summary>
    /// Invokes the inspector-configured UnityEvent when the subject event is 
    /// raised.
    /// </summary>
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
