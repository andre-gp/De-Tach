using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Green.png")]
	[AddComponentMenu("DeTach/Listeners/ColorListener")]
	public class ColorListener : GenericListener<Color, ColorEvent, ColorVariable, ColorUnityEvent> { }

	[System.Serializable]
	public class ColorUnityEvent : UnityEvent<Color> { }
}