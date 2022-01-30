using UnityEngine;

/// <summary>
/// Scriptable object variable to represent a string.
/// </summary>
[CreateAssetMenu(fileName = "new String Variable", 
	menuName = "Variables.../String")]
public class StringVariable : ScriptableObject
{
	[Tooltip("String value of the variable.")]
	public string Value;
}

