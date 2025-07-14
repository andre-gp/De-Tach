using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(FloatVariable))]
	public class FloatVariableInspector : VariableInspector<FloatVariable, float, FloatEvent> {}
}