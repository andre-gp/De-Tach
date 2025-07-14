using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(Vector3Event))]
	public class Vector3EventEditor : EventInspector<Vector3, Vector3Event> { }
}