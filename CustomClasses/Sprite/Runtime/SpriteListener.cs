using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
	[Icon("Packages/com.gaton.de-tach/Icons/Listener_Green.png")]
	[AddComponentMenu("DeTach/Listeners/SpriteListener")]
	public class SpriteListener : GenericListener<Sprite, SpriteEvent, SpriteVariable, SpriteUnityEvent> { }

	[System.Serializable]
	public class SpriteUnityEvent : UnityEvent<Sprite> { }
}