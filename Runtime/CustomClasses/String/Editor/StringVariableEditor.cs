using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(StringVariable))]
	public class StringVariableInspector : VariableInspector<StringVariable, string, StringEvent> {}
}