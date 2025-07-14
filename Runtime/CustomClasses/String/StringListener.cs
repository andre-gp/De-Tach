using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	public class StringEventListener : GenericEventListener<string, StringEvent, StringVariable, StringUnityEvent> { }

	[System.Serializable]
	public class StringUnityEvent : UnityEvent<string> { }
}