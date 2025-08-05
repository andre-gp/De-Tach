using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(FloatListener))]
    public class FloatListenerEditor : ListenerInspector<float, FloatEvent, FloatVariable, FloatUnityEvent, FloatListener> { } 
}
