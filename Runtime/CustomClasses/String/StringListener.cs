using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Blue.png")]
	[AddComponentMenu("DeTach/Listeners/StringListener")]
	public class StringListener : GenericListener<string, StringEvent, StringVariable, StringUnityEvent> { }

	[System.Serializable]
	public class StringUnityEvent : UnityEvent<string> { }
}