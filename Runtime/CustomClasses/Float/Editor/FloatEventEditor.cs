using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(FloatEvent))]
	public class FloatEventEditor : EventInspector<float, FloatEvent> { }
}