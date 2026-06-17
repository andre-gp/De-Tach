using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(GameObjectListener))]
    public class GameObjectListenerEditor : ListenerInspector<GameObject, GameObjectEvent, GameObjectVariable, GameObjectUnityEvent, GameObjectListener> { } 
}
