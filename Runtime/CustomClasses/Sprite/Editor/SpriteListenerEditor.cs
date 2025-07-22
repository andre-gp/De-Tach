using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT 
{
    [CustomEditor(typeof(SpriteListener))]
    public class SpriteListenerEditor : ListenerInspector<Sprite, SpriteEvent, SpriteVariable, SpriteUnityEvent, SpriteListener> { } 
}
