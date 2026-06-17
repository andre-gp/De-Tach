using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(GameObjectEvent))]
	public class GameObjectEventEditor : EventInspector<GameObject, GameObjectEvent> { }
}