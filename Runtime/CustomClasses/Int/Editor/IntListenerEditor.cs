using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(IntListener))]
    public class IntListenerEditor : ListenerInspector<int, IntEvent, IntVariable, IntUnityEvent, IntListener> { } 
}
