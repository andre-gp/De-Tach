using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(IntEvent))]
	public class IntEventEditor : EventInspector<int, IntEvent> { }
}