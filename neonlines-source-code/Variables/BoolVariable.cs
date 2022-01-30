using UnityEngine;

/// <summary>
/// Scriptable object variable to represent a boolean.
/// </summary>
[CreateAssetMenu(fileName = "new Bool Variable",
	menuName = "Variables.../Bool")]
public class BoolVariable : ScriptableObject
{
	[Tooltip("Bool value of the variable.")]
	public bool Value;
}
