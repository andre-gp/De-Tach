using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(BoolListener))]
    public class BoolListenerEditor : ListenerInspector<bool, BoolEvent, BoolVariable, BoolUnityEvent, BoolListener> { } 
}
