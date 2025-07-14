using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	public class FloatEventListener : GenericEventListener<float, FloatEvent, FloatVariable, FloatUnityEvent> { }

	[System.Serializable]
	public class FloatUnityEvent : UnityEvent<float> { }
}