using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(SpriteVariable))]
	public class SpriteVariableInspector : VariableInspector<SpriteVariable, Sprite, SpriteEvent> {}
}