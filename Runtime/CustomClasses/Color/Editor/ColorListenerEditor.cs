using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(ColorListener))]
    public class ColorListenerEditor : ListenerInspector<Color, ColorEvent, ColorVariable, ColorUnityEvent, ColorListener> { } 
}
