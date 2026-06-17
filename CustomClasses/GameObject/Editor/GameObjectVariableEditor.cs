using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
	[CustomEditor(typeof(GameObjectVariable))]
	public class GameObjectVariableInspector : VariableInspector<GameObjectVariable, GameObject, GameObjectEvent> {}
}