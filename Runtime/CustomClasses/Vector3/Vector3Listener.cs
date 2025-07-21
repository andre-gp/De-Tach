using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Green.png")]
	[AddComponentMenu("DeTach/Listeners/Vector3Listener")]
	public class Vector3Listener : GenericListener<Vector3, Vector3Event, Vector3Variable, Vector3UnityEvent> { }

	[System.Serializable]
	public class Vector3UnityEvent : UnityEvent<Vector3> { }
}