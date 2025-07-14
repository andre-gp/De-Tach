using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(StringEvent))]
	public class StringEventEditor : EventInspector<string, StringEvent> { }
}