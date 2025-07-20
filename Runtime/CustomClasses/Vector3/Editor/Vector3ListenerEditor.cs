using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(Vector3Listener))]
    public class Vector3ListenerEditor : ListenerInspector<Vector3, Vector3Event, Vector3Variable, Vector3UnityEvent, Vector3Listener> { } 
}
