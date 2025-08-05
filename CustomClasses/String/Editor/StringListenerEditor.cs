using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(StringListener))]
    public class StringListenerEditor : ListenerInspector<string, StringEvent, StringVariable, StringUnityEvent, StringListener> { } 
}
