using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(BoolVariable))]
	public class BoolVariableInspector : VariableInspector<BoolVariable, bool, BoolEvent> {}
}