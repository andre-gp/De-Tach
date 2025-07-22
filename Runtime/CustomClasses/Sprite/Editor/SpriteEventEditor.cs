using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(SpriteEvent))]
	public class SpriteEventEditor : EventInspector<Sprite, SpriteEvent> { }
}