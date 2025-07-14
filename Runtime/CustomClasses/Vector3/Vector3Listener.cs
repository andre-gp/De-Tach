using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	public class Vector3EventListener : GenericEventListener<Vector3, Vector3Event, Vector3Variable, Vector3UnityEvent> { }

	[System.Serializable]
	public class Vector3UnityEvent : UnityEvent<Vector3> { }
}