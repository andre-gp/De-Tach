using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Blue.png")]
	[AddComponentMenu("DeTach/Listeners/IntListener")]
	public class IntListener : GenericListener<int, IntEvent, IntVariable, IntUnityEvent> { }

	[System.Serializable]
	public class IntUnityEvent : UnityEvent<int> { }
}