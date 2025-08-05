using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(Vector3Variable))]
	public class Vector3VariableInspector : VariableInspector<Vector3Variable, Vector3, Vector3Event> {}
}