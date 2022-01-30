using UnityEngine;

/// <summary>
/// Scriptable object variable to represent a float.
/// </summary>
[CreateAssetMenu(fileName = "new Float Variable",
	menuName = "Variables.../Float")]
public class FloatVariable : ScriptableObject
{
	[Tooltip("Float value of the variable.")]
	public float Value;
}
