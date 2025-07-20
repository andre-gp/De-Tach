using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Blue.png")]
	[AddComponentMenu("DeTach/Listeners/FloatListener")]
	public class FloatListener : GenericListener<float, FloatEvent, FloatVariable, FloatUnityEvent> { }

	[System.Serializable]
	public class FloatUnityEvent : UnityEvent<float> { }
}