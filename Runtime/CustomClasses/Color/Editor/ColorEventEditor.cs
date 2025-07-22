using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(ColorEvent))]
	public class ColorEventEditor : EventInspector<Color, ColorEvent> { }
}