using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	public class IntEventListener : GenericEventListener<int, IntEvent, IntVariable, IntUnityEvent> { }

	[System.Serializable]
	public class IntUnityEvent : UnityEvent<int> { }
}