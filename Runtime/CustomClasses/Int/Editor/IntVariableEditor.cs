using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(IntVariable))]
	public class IntVariableInspector : VariableInspector<IntVariable, int, IntEvent> {}
}