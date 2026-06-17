using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Green.png")]
	[AddComponentMenu("DeTach/Listeners/GameObjectListener")]
	public class GameObjectListener : GenericListener<GameObject, GameObjectEvent, GameObjectVariable, GameObjectUnityEvent> { }

	[System.Serializable]
	public class GameObjectUnityEvent : UnityEvent<GameObject> { }
}