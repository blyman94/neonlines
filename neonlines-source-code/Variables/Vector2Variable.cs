using UnityEngine;

/// <summary>
/// Scriptable object variable to represent a Vector2.
/// </summary>
[CreateAssetMenu(fileName = "new Vector2 Variable",
	menuName = "Variables.../Vector2")]
public class Vector2Variable : ScriptableObject
{
	[Tooltip("Vector2 value of the variable.")]
	public Vector2 Value;
}
