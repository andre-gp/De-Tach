using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(BoolEvent))]
	public class BoolEventEditor : EventInspector<bool, BoolEvent> { }
}