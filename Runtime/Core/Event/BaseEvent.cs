using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Event_White.png")]
    public class BaseEvent : DeTachObject
    {
        public string eventName;

        /// <summary>
        /// Subscribe to this event when you don't care about the value raised.
        /// </summary>
        public event Action OnChangeBaseEvent;

#if UNITY_EDITOR
        [SerializeField] List<UnityEngine.Object> listeners;

        public List<UnityEngine.Object> Listeners
        {
            get
            {
                if (listeners == null)
                    listeners = new List<UnityEngine.Object>();

                return listeners;
            }
        }

#endif
        protected void Invoke()
        {
            OnChangeBaseEvent?.Invoke();
        }
    }
}
