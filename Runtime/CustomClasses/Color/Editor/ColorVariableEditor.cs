using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(ColorVariable))]
	public class ColorVariableInspector : VariableInspector<ColorVariable, Color, ColorEvent> {}
}