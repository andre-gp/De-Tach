using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
    public class GenericEventListener<T, TEvent, TVariable, TUnityEvent> : MonoBehaviour
                                                                where TEvent : GenericEvent<T>
                                                                where TVariable : GenericVariable<T, TEvent>
                                                                where TUnityEvent : UnityEvent<T>
    {
        public TEvent Event;

        public TVariable Variable;

        public TUnityEvent Listeners;

        private void OnEnable()
        {
            if (Event != null)
                Event.Add(OnValueChanged);

            if (Variable != null)
            {
                Listeners.Invoke(Variable.Value);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
                Event.Remove(OnValueChanged);
        }

        private void OnValueChanged(T value)
        {
            Listeners?.Invoke(value);
        }
    }
}
