using UnityEngine;

/// <summary>
/// Scriptable object variable to represent an integer.
/// </summary>
[CreateAssetMenu(fileName = "new Int Variable", 
	menuName = "Variables.../Int")]
public class IntVariable : ScriptableObject
{
	[Tooltip("Integer value of the variable.")]
	public int Value;
}
