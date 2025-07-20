using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
    public class GenericListener<T, TEvent, TVariable, TUnityEvent> : BaseListener
                                                                where TEvent : GenericEvent<T>
                                                                where TVariable : GenericVariable<T, TEvent>
                                                                where TUnityEvent : UnityEvent<T>
    {
        public DeTachType type;

        public DeTachObject deTachObject;

        public TUnityEvent Responses;

        private void OnEnable()
        {
            TEvent deTachEvent = GetEvent();

            if(deTachEvent != null)
            {
                deTachEvent.Add(OnValueChanged);
            }

            if(deTachObject is TVariable variable)
            {
                Responses.Invoke(variable.Value);
            }
        }

        private void OnDisable()
        {
            TEvent deTachEvent = GetEvent();

            if (deTachEvent != null)
            {
                deTachEvent.Remove(OnValueChanged);
            }
        }

        private void OnValueChanged(T value)
        {
            Responses?.Invoke(value);
        }

        private TEvent GetEvent()
        {
            if (deTachObject is TEvent deTachEvent)
                return deTachEvent;

            if(deTachObject is TVariable variable)
                return variable.Event;

            return null;
        }
    }
}
