using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Blue.png")]
	[AddComponentMenu("DeTach/Listeners/BoolListener")]
	public class BoolListener : GenericListener<bool, BoolEvent, BoolVariable, BoolUnityEvent> { }

	[System.Serializable]
	public class BoolUnityEvent : UnityEvent<bool> { }
}