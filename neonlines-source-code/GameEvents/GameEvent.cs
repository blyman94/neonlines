using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event object for the ScriptableObject-based implementation of an event
/// system. Allows for registering and unregistering of listeners. When the 
/// event is raised, it traverses the list and calls each listener's response.
/// </summary>
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// List to store registered listeners.
    /// </summary>
    private List<GameEventListener> listeners =
        new List<GameEventListener>();

    /// <summary>
    /// Traverses the list of listeners and calls their response method.
    /// Traversing the list backward accomodates a scenario where a listener's
    /// response is to unregister itself from the event.
    /// </summary>
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    /// <summary>
    /// Adds a listener to the list of registered listeners.
    /// </summary>
    /// <param name="listener">Listener to be added to the list.</param>
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener from the list of registered listeners if it is 
    /// contained in the list of registered listeners.
    /// </summary>
    /// <param name="listener">:istener to be removed from the list.</param>
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
